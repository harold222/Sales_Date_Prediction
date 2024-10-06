
# Sales Date Prediction

Para este ejercicio se creo un procedimiento almacenado que recibe el id del cliente para realizar la prediccion

```
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE GetCustomerOrderPrediction
  @Filter NVARCHAR(40)
AS
BEGIN
	SET NOCOUNT ON;

  WITH Orders AS
	(
		SELECT
			custid,
			COALESCE(DATEDIFF(DAY, orderdate, LEAD(orderdate) OVER (PARTITION BY custid ORDER BY orderdate)), 0) AS DaysOrders
		FROM SALES.Orders
	),
	AverageDays AS
	(
		SELECT 
			custid, 
			AVG(DaysOrders) AS Average 
		FROM Orders
		GROUP BY custid
	),
	MaximumDate AS
	(
		SELECT
			custid,
			MAX(A.orderdate) AS LastOrderDate
		FROM SALES.Orders A
		GROUP BY custid
	)
	SELECT 
		C.custid,
		C.companyname AS CustomerName,
		FORMAT(MD.LastOrderDate, 'yyyy-MM-dd HH:mm:ss.fff') AS LastOrderDate,
		CASE 
			WHEN AD.Average = 0 THEN NULL
			ELSE FORMAT(DATEADD(DAY, AD.Average, MD.LastOrderDate), 'yyyy-MM-dd HH:mm:ss.fff')  
		END AS NextPredictedOrder
	FROM 
		AverageDays AD
	INNER JOIN MaximumDate MD 
		ON AD.custid = MD.custid
	INNER JOIN Sales.Customers C 
		ON AD.custid = C.custid
	WHERE 
		C.companyname LIKE '%' + @Filter + '%';
END
GO
```
Invación del procedimiento
``` EXEC GetCustomerOrderPrediction 13 ```

Para la realización de las consultas se agrego una columna calculada a la tabla HR.Employees para obtener el nombre completo
```
ALTER TABLE HR.Employees
ADD FullName AS (firstname + ' ' + lastname);
```

Se creo un procedimiento almacenado para la creacion de ordenes con su detalle
```
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE CreateNewOrder
	@Empid INT,
	@Shipperid INT,
	@Shipname NVARCHAR(40),
	@Shipaddress NVARCHAR(60),
	@Shipcity NVARCHAR(15),
	@Orderdate DATETIME,
	@Requireddate DATETIME,
	@Shippeddate DATETIME,
	@Freight MONEY,
	@Shipcountry NVARCHAR(15),
	@ProductId INT,
	@UnitPrice MONEY,
	@Quantity INT,
	@Discount NUMERIC(4,3),
  @CustomerId INT,
	@NewOrderId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRANSACTION;

		INSERT INTO Sales.Orders 
			(empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry, custid)
		VALUES 
			(@Empid, @Shipperid, @Shipname, @Shipaddress, @Shipcity, @Orderdate, @Requireddate, @Shippeddate, @Freight, @Shipcountry, @CustomerId);

		SET @NewOrderId = SCOPE_IDENTITY(); 

		INSERT INTO Sales.OrderDetails
			(orderid, productid, unitprice, qty, discount) 
		VALUES 
			(@NewOrderId, @ProductId, @UnitPrice, @Quantity, @Discount);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
	END CATCH
END
GO
```

## Web Api
Para la realización de la Web Api se creo un proyecto en .Net v8 se utilizo una arquitectura limpia con patron CQRS y Mediator, se utilizaron principios solid con un manejador de errores global, se integro la libreria dapper, fluentvalidation y automapper, se incluyo soporte con Docker y docker-compose para la ejecución de la aplicación, se incluyo el archivo setup.sql con todo la estructura de la base de datos, datos de pruebas y modificaciones realizadas.

Para la ejecución de la aplicación se crearon dos maneras en el archivo launchSettings, ejecutar el proyecto con la configuracion https que no utilizara docker por lo cual es necesario tener en cuenta los cambios de la base de datos descritos anteriormente o ejecutar mediante docker-compose up que se encargara de levantar la base de datos y la aplicación, aca se aplicaran los cambios a la base de datos mediante script para que pueda ser probada la aplicación sin necesidad de realizar cambios manuales.

Los endpoints creados son:
- api/Services/CreateOrder = Permite la creación de una orden con su detalle
- api/Services/NextOrder = Permite obtener la prediccion de la siguiente orden de cada cliente
- api/Services/GetProducts = Permite obtener la lista de productos
- api/Services/GetShippers = Permite obtener la lista de transportadores
- api/Services/GetOrderByClient/{Id} = Permite obtener las ordenes de un cliente
- api/Services/GetEmployees = Permite obtener la lista de empleados
La url base depende de la configuración que se utilice para la ejecución de la aplicación, si es con
- https: https://localhost:7128/
- docker: https://localhost:6060/
 
Se utiliza XUnit para realizar pruebas de los servicios

## Web App
Para esto se utilizo angular en la version 18 con angular material, se usaron los mismos componentes que el mockup, se usaron variables de entorno para guardar la url de la api a consumir en el archivo .env se dejo apuntando a docker
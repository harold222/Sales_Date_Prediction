(() => {
  const setMargin = 20;
  const colors = ['#ff6084', '#37c2eb', '#bbce56', '#4bc0c0', '#0066ff'];

  function setAlertMessage(message) {
    alert(message);
  }

  document.addEventListener('DOMContentLoaded', () => {

    const button = document.getElementById('update-button');

    if (!button) {
      alert('No se encontró el botón para actualizar');
      return
    }
      
    button.addEventListener('click', () => {
      d3.select('#chart').selectAll('*').remove();

      const text = document.getElementById('data-input');
      if (!text) return setAlertMessage('No se encontró el input de texto');
      
      const inputData = text.value;
      if (!inputData) return setAlertMessage('No se ingresó ningún dato');

      const validPattern = /^[0-9.,]*$/.test(inputData);
      if (!validPattern) return setAlertMessage("Solo se permiten números positivos, decimales y comas");

      const data = inputData.split(',').map(d => parseFloat(d.trim())).filter(d => !isNaN(d));

      const margin = 
        { top: setMargin, right: setMargin, bottom: setMargin, left: setMargin },
        width = 600 - margin.left - margin.right,
        height = 500 - margin.top - margin.bottom;

      // Crear SVG
      const svg = d3.select('#chart')
          .append('svg')
          .attr('width', width + margin.left + margin.right)
          .attr('height', height + margin.top + margin.bottom)
          .append('g')
          .attr('transform', `translate(${margin.left},${margin.top})`);

      const x = d3.scaleLinear()
        .domain([0, d3.max(data)])
        .range([0, width]);

      const y = d3.scaleBand()
        .domain(data.map((_, i) => i))
        .range([0, height])
        .padding(0.1);

      // Crear barras
      svg.selectAll('.bar')
          .data(data)
          .enter()
          .append('rect')
          .attr('class', 'bar')
          .attr('x', 0)
          .attr('y', (d, i) => y(i)) 
          .attr('width', d => x(d))
          .attr('height', y.bandwidth())
          .attr('fill', (d, i) => colors[i % colors.length]);

      // Crear texto
      svg.selectAll('.text')
            .data(data)
            .enter()
            .append('text')
            .attr('class', 'text')
            .attr('x', d => x(d) - 5)
            .attr('y', (d, i) => y(i) + y.bandwidth() / 2)
            .attr('dy', '.35em')
            .attr('text-anchor', 'end')
            .attr('fill', 'white')
            .text(d => d);
    });
  });
})();
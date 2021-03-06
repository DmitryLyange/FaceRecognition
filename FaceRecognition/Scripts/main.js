window.onload = () => {
  window.currentState = {};
};

async function runCnn(event) {
  try {
    //get the response data by algorigth name
    const responseData = await apiCall(
      `Api/Classifier/CNN/FiveSmall/${currentState.photoId ||
        "Alvaro_Uribe_0012"}`
    );
    var plot = JSON.parse(responseData);
    //create new card by reseved algorithm data
    createCNNImageCard(plot.Results);
  } catch (e) {
    console.log(e);
  }
}

async function runObr(event) {
  try {
    //get the response data by algorigth name
    const responseData = await apiCall(
      `Api/Classifier/OpenBR/FiveSmall/${currentState.photoId ||
        "Alvaro_Uribe_0012"}`
    );
    var plot = JSON.parse(responseData);
    //create new card by reseved algorithm data
    createObrImageCard(plot.Results);
  } catch (e) {
    console.log(e);
  }
}

async function getConfTable(event) {
  try {
    //get the response data by algorigth name
    const responseData = await apiCall("Api/PerformanceMetric/Table/PCA");
    const data = JSON.parse(responseData);
    //create new card by reseved algorithm data

    createNewDataView(data.Success === false ? [] : data);
    if (data.Success === false) {
      console.error(new Error(data.Error.Message));
    }
  } catch (e) {
    console.log(e);
  }
}

async function getPmGraph(event) {
  // examples:
  //const responseData = await apiCall('Api/PerformanceMetric/Table/PCA');
  //const responseData = await apiCall('Api/PerformanceMetric/Data/PCA');
  //const responseData = await apiCall('Api/PerformanceMetric/Graph/Precision');
  try {
    //get the response data by algorigth name
    const responseData = await apiCall(
      `Api/PerformanceMetric/Graph/${currentState.metric || "Precision"}`
    );
    const data = JSON.parse(responseData);

    //const responseData = await apiCall('Api/Classifier/CNN/FiveSmall/Alvaro_Uribe_0012');
    var line = britecharts.line();
    line
      .isAnimated(true)
      .aspectRatio(0.5)
      .grid("horizontal")
      .tooltipThreshold(600)
      .margin(50)
      .height(500)
      .width(1000)
      .xAxisLabel(data.XAxis)
      .yAxisLabel(data.YAxis)
      .yAxisLabelPadding(50)
      .dateLabel("date");

    var tData = data.Graphs.map(graph => ({
      topicName: graph.GraphName,
      topic: graph.GraphName,
      dates: graph.GraphLines.map((point, i) => ({
        date: new Date(new Date().setDate(i)),
        value: point.Y
      }))
    }));

    d3
      .select("#grathContainer")
      .datum({ dataByTopic: tData })
      .call(line);
  } catch (e) {
    console.log(e);
  }
}

function createNewDataView(data = []) {
  data.forEach(DataView => {
    createNewTable(DataView.ContingencyTable);
    createSimpleTable(DataView.PerformanceRates);
    createRocAuc(DataView.RocCurve);
  });
}

function createGraph(event) {
  drawGrapt();
}

function createRocAuc(rocData) {
  drawRocAuc(rocData);
}

async function apiCall(url) {
  const data = await fetch(url, {
    method: "get",
    contentType: "json",
    dataType: "json"
  });
  if (data) {
    //here goes the response
    //TODO apply JSON.parse(data.text())
    return await data.text();
  } else {
    throw new Error("Scheisse!!");
  }
}

function createNewStatisticsCard(plot) {
  const nodePrototype = document.getElementById("cardContainer");
  const clonedElement = nodePrototype.cloneNode(true);

  try {
    //set up the card values
    clonedElement["fce"].value = plot["FirstTypeErrors"];
    clonedElement["sce"].value = plot["SecondTypeErrors"];
    clonedElement["speed"].value = plot["LearningSpeed"];
    clonedElement["accuracy"].value = plot["RecognizingSpeed"];
  } catch (e) {}
  //add card to the container
  document.getElementById("cardContainer").appendChild(clonedElement);
  //show new card
  clonedElement.style.display = "block";
}

function createNewTable(tableData) {
  const nodePrototype = document.getElementById("tableTamplate");
  const clonedElement = nodePrototype.cloneNode(true);

  try {
    //set up the card values
    clonedElement.querySelector(".cell1-grid-cell").innerHTML = tableData[0];
    clonedElement.querySelector(".cell2-grid-cell").innerHTML = tableData[1];
    clonedElement.querySelector(".cell3-grid-cell").innerHTML = tableData[2];
    clonedElement.querySelector(".cell4-grid-cell").innerHTML = tableData[3];
    clonedElement.querySelector(".cell5-grid-cell").innerHTML = tableData[4];
    clonedElement.querySelector(".cell6-grid-cell").innerHTML = tableData[5];
    clonedElement.querySelector(".cell7-grid-cell").innerHTML = tableData[6];
    clonedElement.querySelector(".cell8-grid-cell").innerHTML = tableData[7];
    clonedElement.querySelector(".cell9-grid-cell").innerHTML = tableData[8];
    clonedElement.querySelector(".cell10-grid-cell").innerHTML = tableData[9];
    clonedElement.querySelector(".cell11-grid-cell").innerHTML = tableData[10];
  } catch (e) {}
  //add card to the container
  document.getElementById("cardContainer").appendChild(clonedElement);
  //show new card
  clonedElement.style.display = "block";
}

function createSimpleTable(tableData) {
  const nodePrototype = document.getElementById("tableTamplateNew");
  const clonedElement = nodePrototype.cloneNode(true);

  try {
    //set up the card values
    clonedElement.querySelector(".cell1-grid-cell-new").innerHTML =
      tableData[0];
    clonedElement.querySelector(".cell2-grid-cell-new").innerHTML =
      tableData[1];
    clonedElement.querySelector(".cell3-grid-cell-new").innerHTML =
      tableData[2];
    clonedElement.querySelector(".cell4-grid-cell-new").innerHTML =
      tableData[3];
    clonedElement.querySelector(".cell5-grid-cell-new").innerHTML =
      tableData[4];
    clonedElement.querySelector(".cell6-grid-cell-new").innerHTML =
      tableData[5];
    clonedElement.querySelector(".cell7-grid-cell-new").innerHTML =
      tableData[6];
    clonedElement.querySelector(".cell8-grid-cell-new").innerHTML =
      tableData[7];
    clonedElement.querySelector(".cell9-grid-cell-new").innerHTML =
      tableData[8];
    clonedElement.querySelector(".cell10-grid-cell-new").innerHTML =
      tableData[9];
    clonedElement.querySelector(".cell11-grid-cell-new").innerHTML =
      tableData[10];
  } catch (e) {}
  //add card to the container
  document.getElementById("cardContainer").appendChild(clonedElement);
  //show new card
  clonedElement.style.display = "block";
}

function drawGrapt() {
  //here goes grath data
  //now it is letter for x-axis, and frequency for y-axis
  const data = [
    {
      letter: "A",
      frequency: "0.08167"
    },
    {
      letter: "B",
      frequency: "0.1492"
    }
  ];
  //technical staff
  var svg = d3.select("svg"),
    margin = { top: 20, right: 20, bottom: 30, left: 40 },
    width = +svg.attr("width") - margin.left - margin.right,
    height = +svg.attr("height") - margin.top - margin.bottom;

  var x = d3
      .scaleBand()
      .rangeRound([0, width])
      .padding(0.1),
    y = d3.scaleLinear().rangeRound([height, 0]);

  var g = svg
    .append("g")
    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

  //parse data to axis
  x.domain(data.map(d => d.letter));
  y.domain([0, d3.max(data, d => d.frequency)]);

  //technical staff
  g
    .append("g")
    .attr("class", "axis axis--x")
    .attr("transform", "translate(0," + height + ")")
    .call(d3.axisBottom(x));

  g
    .append("g")
    .attr("class", "axis axis--y")
    .call(d3.axisLeft(y).ticks(10, "%"))
    .append("text")
    .attr("transform", "rotate(-90)")
    .attr("y", 6)
    .attr("dy", "0.71em")
    .attr("text-anchor", "end")
    .text("Frequency");

  g
    .selectAll(".bar")
    .data(data)
    .enter()
    .append("rect")
    .attr("class", "bar")
    .attr("x", function(d) {
      return x(d.letter);
    })
    .attr("y", function(d) {
      return y(d.frequency);
    })
    .attr("width", x.bandwidth())
    .attr("height", function(d) {
      return height - y(d.frequency);
    });
}

function drawRocAuc(rocData) {
  // set the dimensions and margins of the graph
  var margin = { top: 20, right: 150, bottom: 30, left: 50 },
    width = 960 - margin.left - margin.right,
    height = 470 - margin.top - margin.bottom;

  // array of curve functions and tites
  var curveArray = [
    { d3Curve: d3.curveLinear, curveTitle: "curveLinear" },
    { d3Curve: d3.curveStep, curveTitle: "curveStep" },
    { d3Curve: d3.curveStepBefore, curveTitle: "curveStepBefore" },
    { d3Curve: d3.curveStepAfter, curveTitle: "curveStepAfter" },
    { d3Curve: d3.curveBasis, curveTitle: "curveBasis" },
    { d3Curve: d3.curveCardinal, curveTitle: "curveCardinal" },
    { d3Curve: d3.curveMonotoneX, curveTitle: "curveMonotoneX" },
    { d3Curve: d3.curveCatmullRom, curveTitle: "curveCatmullRom" }
  ];

  // parse the date / time
  var parseTime = d3.timeParse("%d-%b-%y");

  // set the ranges
  var x = d3.scaleTime().range([0, width]);
  var y = d3.scaleLinear().range([height, 0]);

  // define the line
  var valueline = d3
    .line()
    .curve(d3.curveCatmullRomOpen)
    .x(function(d) {
      return x(d.date);
    })
    .y(function(d) {
      return y(d.close);
    });

  // append the svg obgect to the body of the page
  // appends a 'group' element to 'svg'
  // moves the 'group' element to the top left margin
  var svg = d3
    .select("body")
    .append("svg")
    .attr("width", width + margin.left + margin.right)
    .attr("height", height + margin.top + margin.bottom)
    .append("g")
    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

  // Get the data
  d3.csv("data-3.csv", function(error, data) {
    if (error) throw error;

    // format the data
    data.forEach(function(d) {
      d.date = parseTime(d.date);
      d.close = +d.close;
    });

    // set the colour scale
    var color = d3.scaleOrdinal(d3.schemeCategory10);

    curveArray.forEach(function(daCurve, i) {
      // Scale the range of the data
      x.domain(
        d3.extent(data, function(d) {
          return d.date;
        })
      );
      y.domain(
        d3.extent(data, function(d) {
          return d.close;
        })
      );

      // Add the paths with different curves.
      svg
        .append("path")
        .datum(data)
        .attr("class", "line")
        .style("stroke", function() {
          // Add the colours dynamically
          return (daCurve.color = color(daCurve.curveTitle));
        })
        .attr("id", "tag" + i) // assign ID
        .attr(
          "d",
          d3
            .line()
            .curve(daCurve.d3Curve)
            .x(function(d) {
              return x(d.date);
            })
            .y(function(d) {
              return y(d.close);
            })
        );

      // Add the Legend
      svg
        .append("text")
        .attr("x", width + 5) // space legend
        .attr("y", margin.top + 20 + i * 20)
        .attr("class", "legend") // style the legend
        .style("fill", function() {
          // Add the colours dynamically
          return (daCurve.color = color(daCurve.curveTitle));
        })
        .on("click", function() {
          // Determine if current line is visible
          var active = daCurve.active ? false : true,
            newOpacity = active ? 0 : 1;
          // Hide or show the elements based on the ID
          d3
            .select("#tag" + i)
            .transition()
            .duration(100)
            .style("opacity", newOpacity);
          // Update whether or not the elements are active
          daCurve.active = active;
        })
        .text(daCurve.curveTitle);
    });

    // Add the scatterplot
    svg
      .selectAll("dot")
      .data(data)
      .enter()
      .append("circle")
      .attr("r", 4)
      .attr("cx", function(d) {
        return x(d.date);
      })
      .attr("cy", function(d) {
        return y(d.close);
      });

    // Add the X Axis
    svg
      .append("g")
      .attr("class", "axis")
      .attr("transform", "translate(0," + height + ")")
      .call(d3.axisBottom(x));

    // Add the Y Axis
    svg
      .append("g")
      .attr("class", "axis")
      .call(d3.axisLeft(y));
  });
}

function createCNNImageCard(data) {
  const sortedData = data.sort((a, b) => b.Probability - a.Probability);

  document.getElementById("imageContainer").insertAdjacentHTML(
    "afterbegin",
    `
    <div class='imageWrap'>
      <div class='mainImage'>
        <span class='imageTitle'>Best match: ${
          sortedData[0].Probability
        }%</span>
        <img src='images/${sortedData[0].ClassLabel}/${
      sortedData[0].ClassLabel
    }_0001.jpg'>
      </div>
      <div class='subImages'>
      <span class='imageTitle'>Other matches</span>
      <div class='subImagesWrp'>
      <div class='contentImg'>
      <span class='imageTitle'>${sortedData[1].Probability}%</span>
        <img src='images/${sortedData[1].ClassLabel}/${
      sortedData[1].ClassLabel
    }_0001.jpg'>
        </div>
        <div class='contentImg'>
        <span class='imageTitle'>${sortedData[2].Probability}%</span>
        <img src='images/${sortedData[2].ClassLabel}/${
      sortedData[2].ClassLabel
    }_0001.jpg'>
        </div>
        <div class='contentImg'>
        <span class='imageTitle'>${sortedData[3].Probability}%</span>
        <img src='images/${sortedData[3].ClassLabel}/${
      sortedData[3].ClassLabel
    }_0001.jpg'>
        </div>
        <div class='contentImg'>
        <span class='imageTitle'>${sortedData[4].Probability}%</span>
        <img src='images/${sortedData[4].ClassLabel}/${
      sortedData[4].ClassLabel
    }_0001.jpg'>
        </div>
        </div>
      </div>
    </div>
    `
  );
}

function createObrImageCard(data) {
  const sortedData = data.sort((a, b) => b.Probability - a.Probability);

  document.getElementById("imageContainer").insertAdjacentHTML(
    "afterbegin",
    `
    <div class='imageWrap'>
      <div class='mainImage'>
        <span class='imageTitle'>Best match: ${
          sortedData[0].Probability
        }%</span>
        <img src='images/${sortedData[0].FileName.substring(
          0,
          sortedData[0].FileName.length - 5
        )}/${sortedData[0].FileName}.jpg'>
      </div>
      <div class='subImages'>
      <span class='imageTitle'>Other matches</span>
      <div class='subImagesWrp'>
      <div class='contentImg'>
      <span class='imageTitle'>${sortedData[1].Probability}%</span>
        <img src='images/${sortedData[1].FileName.substring(
          0,
          sortedData[1].FileName.length - 5
        )}/${sortedData[1].FileName}.jpg'>
        </div>
        <div class='contentImg'>
        <span class='imageTitle'>${sortedData[2].Probability}%</span>
        <img src='images/${sortedData[2].FileName.substring(
          0,
          sortedData[2].FileName.length - 5
        )}/${sortedData[2].FileName}.jpg'>
        </div>
        <div class='contentImg'>
        <span class='imageTitle'>${sortedData[3].Probability}%</span>
        <img src='images/${sortedData[3].FileName.substring(
          0,
          sortedData[3].FileName.length - 5
        )}/${sortedData[3].FileName}.jpg'>
        </div>
        <div class='contentImg'>
        <span class='imageTitle'>${sortedData[4].Probability}%</span>
        <img src='images/${sortedData[4].FileName.substring(
          0,
          sortedData[4].FileName.length - 5
        )}/${sortedData[4].FileName}.jpg'>
        </div>
        </div>
      </div>
    </div>
    `
  );
}

function setFileState(file) {
  !window.currentState && (window.currentState = {});

  currentState.photoId = file[0].name.substring(0, file[0].name.length - 4);
}

function setMetricsState(id) {
  !window.currentState && (window.currentState = {});

  currentState.metric = id;
}

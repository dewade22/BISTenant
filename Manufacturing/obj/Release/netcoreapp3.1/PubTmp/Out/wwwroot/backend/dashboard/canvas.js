//untuk chart JS
let tanggal = new Date();
//definisi= tenant dan base url
let tenant = $('#tenantActive').val().trim();
let baseurl = window.location.origin;
let alamat = baseurl + '/' + tenant + '/Dashboard';
getData();

//definisi canvas
let ctx = $("#myChart");
const forecast = [];
const labels = [];
const real = [];
const last = [];
function getData() {
    let tahun = tanggal.getFullYear();
    $.ajax({
        type: "POST",
        url: alamat+"/DataSales?tahun=" + tahun,
        success: function (res) {
            $.each(res.label, function (i) {
                labels.push(res.label[i]);
                forecast.push(parseFloat(res.data[i]));
                real.push(parseFloat(res.invoice[i]));
                last.push(parseFloat(res.lastYear[i]));
            })
        }
    }).done(function () {

        var option = {
            showLines: true,
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (value, index, values) {
                            // Convert the number to a string and splite the string every 3 charaters
                            value = value.toString();
                            value = value.split(/(?=(?:...)*$)/);

                             // Convert the array to a string and format the output
                            value = value.join('.');
                            return 'Rp. ' + value;
                        },
                        tooltipTemplate: function (valueObject) {
                            return valueObject.label + ':Rp. ' + valueObject.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        }
                    }
                }]
            },   
        };
       
        var myChart = new Chart(ctx, {
            type: "line",
            data: {
                labels: labels,
                datasets: [
                    {
                        label: "Forecast",
                        fill: false,
                        lineTension: 0.1,
                        backgroundColor: "#18F608",
                        borderColor: "#84D67F",
                        data: forecast,
                    },

                    {
                        label: "This Year",
                        fill: false,
                        lineTension: 0.1,
                        backgroundColor: "#2516F7",
                        borderColor: "#827CDE",
                        data: real,
                    },

                    {
                        label: "Last Year",
                        fill: false,
                        lineTension: 0.1,
                        backgroundColor: "#F7F416",
                        borderColor: "#64621B",
                        data: last,
                    }
                ]
            },
            options: option
        });
    })
}
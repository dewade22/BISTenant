let param = window.location.search;
let baseurl = window.location.origin;

$(document).ready(function () {
    Sales();
})
function Sales() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: baseurl + '/BalimoonBIP/SalesActual/TodaySalesDataBIP' + param,
        success: function (result) {
            for (let i = 0; i < result.length; i++) {
                let transaksi = `<tr>
                                    <td>${result[i].soNumber}</td>
                                    <td>${result[i].documentNo}</td>
                                    <td>${result[i].salesPerson}</td>
                                    <td>${result[i].billtoName}</td>
                                    <td>${result[i].category}</td>
                                    <td>${result[i].qty}</td>
                                    <td>${result[i].liters}</td>
                                    <td>${result[i].cost}</td>
                                    <td>${result[i].amount}</td>
                                    <td>${result[i].discount}</td>
                                    <td>${result[i].tax}</td>
                                    <td>${result[i].amountIncdTax}</td>
                                    <td>${result[i].landedCost}</td>
                                    <td>${result[i].revenue}</td>
                                 </tr>`
                jQuery("#tableSales tbody").append(transaksi);
            }

        }, 
        error: function (jqXHR, exception) {
            Swal.fire(
                'Error !',
                '' + exception + ' ' + jqXHR.status,
                'error'
            )
        }
    }).done(function () {
        initDataTable();
    })
}

function initDataTable() {
    $('#tableSales').bootgrid({
        caseSensitive: false,
        formatters: {
            'liters': function (column, row) {
                return `${parseFloat(row.liters).toFixed(2)}`;
            },
            'cost': function (column, row) {
                return `${parseFloat(row.cost).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
            },
            'amount': function (column, row) {
                return `${parseFloat(row.amount).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
            },
            'discount': function (column, row) {
                return `${parseFloat(row.discount).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
            },
            'tax': function (column, row) {
                return `${parseFloat(row.tax).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
            },
            'amountIncdTax': function (column, row) {
                return `${parseFloat(row.amountIncdTax).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
            },
            'landedCost': function (column, row) {
                return `${parseFloat(row.landedCost).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
            },
            'revenue': function (column, row) {
                return `${parseFloat(row.revenue).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
            }
        }
    });
}
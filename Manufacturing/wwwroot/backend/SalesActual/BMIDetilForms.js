let param = window.location.search;
let baseurl = window.location.origin;

$(document).ready(function () {
    Sales();
})
function Sales() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: baseurl + '/BalimoonBMI/SalesActual/TodaySales' + param,
        success: function (result) {
            for (let i = 0; i < result.length; i++) {
                let transaksi = `<tr>
                                    <td>${result[i].soNumber}</td>
                                    <td>${result[i].documentNo}</td>
                                    <td>${result[i].salesPerson}</td>
                                    <td>${result[i].billtoName}</td>
                                    <td>${result[i].qty}</td>
                                    <td>${(result[i].liters).toFixed(2)}</td>
                                    <td>Rp. ${(result[i].cost).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits:2})}</td>
                                    <td>Rp. ${(result[i].amount).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                                    <td>Rp. ${(result[i].discount).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                                    <td>Rp. ${(result[i].tax).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                                    <td>Rp. ${(result[i].amountIncdTax).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                                    <td>Rp. ${(result[i].landedCost).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                                    <td>Rp. ${(result[i].revenue).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                                 </tr>`
                jQuery("#tableSales tbody").append(transaksi);
            }
            
        }
    })
}
let param = window.location.search;
let baseurl = window.location.origin;

$(document).ready(function () {
    getSalesBoard();
})

function getSalesBoard() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: baseurl + '/BalimoonBIP/SalesActual/SalesBoardPerSales' + param,
        success: function (result) {
            let data = result.hasil
            let [LiA, LiB, QtA, QtB, CsA, CsB, ReA, ReB, DLiA, DLiB, DQtA, DQtB, DCsA, DCsB, DReA, DReB] = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
            for (let i = 0; i < data.length; i++) {
                let tables = `<table id="table${data[i].itemCategory}" class="table table-valign-middle table-bordered table-hover" style="table-layout:fixed; width:100%">
                                <thead>
                                    <tr>
                                        <th width="150px">${data[i].itemCategory} - ${data[i].salesPerson}</th>
                                        <th width="200px">Target</th>
                                        <th width="200px">Achieved</th>
                                        <th width="200px">% Diff</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Liters</td>
                                        <td>${data[i].litersBudget.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].litersMonth.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].litersBudget == 0 ? `0` : `${Math.round(((data[i].litersMonth / data[i].litersBudget * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Quantity</td>
                                        <td>${data[i].qtyBudget}</td>
                                        <td>${data[i].qtyMonth}</td>
                                        <td>${data[i].qtyBudget == 0 ? `0` : `${Math.round(((data[i].qtyMonth / data[i].qtyBudget * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Cases</td>
                                        <td>${(data[i].litersBudget / 8.4).toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${(data[i].litersMonth / 8.4).toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].litersBudget == 0 ? `0` : `${Math.round((((data[i].litersMonth / 8.4) / (data[i].litersBudget / 8.4) * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Revenue</td>
                                        <td>${data[i].revenueBudget.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].revenueMonth.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].revenueBudget == 0 ? `0` : `${Math.round(((data[i].revenueMonth / data[i].revenueBudget * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Daily Liters</td>
                                        <td>${(data[i].litersBudget / data[i].daysMonth).toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].litersDay.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].litersBudget == 0 ? `0` : `${Math.round(((data[i].litersDay / (data[i].litersBudget / data[i].daysMonth) * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Daily Quantity</td>
                                        <td>${(data[i].qtyBudget / data[i].daysMonth).toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].qtyDay}</td>
                                        <td>${data[i].qtyBudget == 0 ? `0` : `${Math.round(((data[i].qtyDay / (data[i].qtyBudget / data[i].daysMonth) * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Daily Cases</td>
                                        <td>${((data[i].litersBudget / data[i].daysMonth) / 8.4).toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${(data[i].litersDay / 8.4).toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].litersBudget == 0 ? `0` : `${Math.round((((data[i].litersDay / 8.4) / ((data[i].litersBudget / data[i].daysMonth) / 8.4) * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Daily Revenue</td>
                                        <td>${(data[i].revenueBudget / data[i].daysMonth).toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].revenueDay.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${data[i].revenueBudget == 0 ? `0` : `${Math.round(((data[i].revenueDay / (data[i].revenueBudget / data[i].daysMonth) * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                </tbody>
                             </table>
                             <br><br>`

                jQuery("#tempatTable").append(tables)
                LiA += data[i].litersBudget
                LiB += data[i].litersMonth
                QtA += data[i].qtyBudget
                QtB += data[i].qtyMonth
                CsA += (data[i].litersBudget / 8.4)
                CsB += (data[i].litersMonth / 8.4)
                ReA += data[i].revenueBudget
                ReB += data[i].revenueMonth
                DLiA += (data[i].litersBudget / data[i].daysMonth)
                DLiB += data[i].litersDay
                DQtA += (data[i].qtyBudget / data[i].daysMonth)
                DQtB += data[i].qtyDay
                DCsA += ((data[i].litersBudget / data[i].daysMonth) / 8.4)
                DCsB += (data[i].litersDay / 8.4)
                DReA += (data[i].revenueBudget / data[i].daysMonth)
                DReB += data[i].revenueDay
            }
            let summary = `<table id="TabelSummary" class="table table-valign-middle table-bordered table-hover" style="table-layout:fixed; width:100%">
                                <thead>
                                    <tr>
                                        <th width="150px">Summary - ${data[0].salesPerson}</th>
                                        <th width="200px">Target</th>
                                        <th width="200px">Achieved</th>
                                        <th width="200px">% Diff</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Liters</td>
                                        <td>${LiA.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${LiB.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${LiA == 0 ? `0` : `${Math.round(((LiB / LiA * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Quantity</td>
                                        <td>${QtA}</td>
                                        <td>${QtB}</td>
                                        <td>${QtA == 0 ? `0` : `${Math.round(((QtB / QtA * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Cases</td>
                                        <td>${CsA.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${CsB.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${CsA == 0 ? `0` : `${Math.round(((CsB / CsA * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Revenue</td>
                                        <td>${ReA.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${ReB.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${ReA == 0 ? `0` : `${Math.round(((ReB / ReA * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Daily Liters</td>
                                        <td>${DLiA.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${DLiB.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${DLiA == 0 ? `0` : `${Math.round(((DLiB / DLiA * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Daily Quantity</td>
                                        <td>${DQtA.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${DQtB.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${DQtA == 0 ? `0` : `${Math.round(((DQtB / DQtA * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Daily Cases</td>
                                        <td>${DCsA.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${DCsB.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${DCsA == 0 ? `0` : `${Math.round(((DCsB / DCsA * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                    <tr>
                                        <td>Daily Revenue</td>
                                        <td>${DReA.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${DReB.toLocaleString('en-us', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                        <td>${DReA == 0 ? `0` : `${Math.round(((DReB / DReA * 100) + Number.EPSILON) * 100) / 100}`}</td>
                                    </tr>
                                </tbody>
                           </table>`
            jQuery("#tempatTable").append(summary)
        },
        error: function (jqXHR, exception) {
            Swal.fire(
                'Error !',
                '' + exception + ' ' + jqXHR.status,
                'error'
            )
        }
    })
}
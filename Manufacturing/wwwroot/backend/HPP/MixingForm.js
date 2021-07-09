let baseurl = localStorage.getItem('thisAddress');

$(function () {
    $('#tableItem').bootgrid({
        caseSensitive: false,
        formatters: {
            'Action': function (column, row) {
                return `<button type="button" class="btn btn-icon command-edit waves-effect waves-circle" onclick="PushUpdate('${row.Id}', '${row.ItemDescription}')"><span class="zmdi zmdi-edit"></span></button>` +
                    `<button type="button" class="btn btn-icon command-delete waves-effect" onclick="PushDelete('${row.Id}', '${row.ItemDescription}')"><span class="zmdi zmdi-delete"></span></button>`
            },
        }
    })
    $('#tableFOH').bootgrid({
        caseSensitive: false,
        formatters: {
            'Duration': function (column, row) {
                return `${row.Duration != "" ? parseFloat(row.Duration).toFixed(3) : ""}`
            },
            'Quantity': function (column, row) {
                return `${parseFloat(row.Quantity).toFixed()}`
            },
            'Amount': function (column, row) {
                return `${row.FOHType == 'electricity' ? row.Amount + ' KWh' : row.FOHType == 'gas' ? row.Amount + ' KG' : row.Amount + ' L'}`
            }
        }
    })
})
let baseUrl = localStorage.getItem('thisAddress')
$(function () {
    $('#tableForm').bootgrid({
        caseSensitive: false,
        formatters: {
            'commands': function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"Push(\'" + row.ItemNo + "\')\"><span class=\"zmdi zmdi-check-circle\"></span></button>"
            },
            'Amount': function (column, row) {
                return `${(parseFloat(row.ItemQty) * parseFloat(row.Cost)).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}`
            },
            'cost': function (column, row) {
                return `${parseFloat(row.Cost).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}`
            }
        }
    })
})
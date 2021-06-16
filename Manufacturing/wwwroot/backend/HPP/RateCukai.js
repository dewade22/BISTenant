let baseUrl = localStorage.getItem('thisAddress')

$(function () {
    $('#TablesData').bootgrid({
        caseSensitive: false,
        formatters: {
            'commands': function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"PushUpdate(\'" + row.RateNo + "\',\'" + row.RateName + "\')\"><span class=\"zmdi zmdi-edit\"></span></button>" +
                    "<button type=\"button\" class=\"btn btn-icon command-delete waves-effect waves-circle\" onclick=\"PushDelete(\'" + row.RateNo + "\',\'" + row.RateName + "\')\"><span class=\"zmdi zmdi-delete\"></span></button>"
            }
        }
    })
})

function Add() {
    $('#modalForm').modal()
    $('#updateform').hide()
    $('#AddNew').show()
    $('.fg-line').removeClass('fg-toggled')
    $('.modal-title').html('Add Cukai Rates')
}
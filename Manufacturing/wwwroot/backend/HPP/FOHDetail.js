let baseUrl = localStorage.getItem('thisAddress')
$(function () {
    $('#tabelFOH').bootgrid({
        caseSensitive: false,
        formatters: {
            "commands": function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"PushUpdate(\'" + row.ModelDetailFOHNo + "\')\"><span class=\"zmdi zmdi-edit\"></span></button>" +
                    "<button type=\"button\" class=\"btn btn-icon command-delete waves-effect waves-circle\" onclick=\"PushDelete(\'" + row.ModelDetailFOHNo + "\')\"><span class=\"zmdi zmdi-delete\"></span></button>"
            }
        }
    })
})
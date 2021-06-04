let baseUrl = localStorage.getItem('thisAddress')

$(function () {
    $('#tabelMachine').bootgrid({
        caseSensitive: false,
        formatters: {
            "commands": function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"PushUpdate(\'" + row.MachineNo + "\',\'" + row.MachineName + "\')\"><span class=\"zmdi zmdi-edit\"></span></button>" +
                    "<button type=\"button\" class=\"btn btn-icon command-delete waves-effect waves-circle\" onclick=\"PushDelete(\'" + row.MachineNo + "\',\'" + row.MachineName + "\')\"><span class=\"zmdi zmdi-delete\"></span></button>"
            }
        }
    })

})

function AddNewMachine() {
    $('#modalForm').modal()
    $('#updateform').hide()
    $('#AddNew').show()
    $('.modal-title').html('Add New Machine')
}
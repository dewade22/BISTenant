let baseUrl = localStorage.getItem('thisAddress')
$(function () {
    $('#TablesData').bootgrid({
        caseSensitive: false,
        formatters: {
            "commands": function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"PushUpdate(\'" + row.ModelDetailFOHNo + "\',\'" + row.OperationName + "\',\'" + row.SubProcessName + "\')\"><span class=\"zmdi zmdi-edit\"></span></button>" +
                    "<button type=\"button\" class=\"btn btn-icon command-delete waves-effect waves-circle\" onclick=\"PushDelete(\'" + row.ModelDetailFOHNo + "\',\'" + row.OperationName + "\',\'" + row.SubProcessName + "\')\"><span class=\"zmdi zmdi-delete\"></span></button>"
            },
        }
    })

    //Validasi
    $('#formInput').validate({
        onkeyup: function (element) { $(element).valid() }
    })

    //Jika modal di hide
    $('#modalForm').on('hidden.bs.modal', function () {
        $('#modalForm form')[0].reset()
        $('#Unit').val('')
        $('#Unit').trigger('chosen:updated')
        
    })

    //Simpan form Add
    $('#AddNew').click(function () {
        if ($('#formInput').valid()) {
            if ($('#Unit').val() == '') {
                $('#errorselect').html('Tipe Rate Wajib Diisi')
            }
            else {
                $.ajax({
                    type: 'POST',
                    url: baseUrl + '/HPPItem/RateLabour',
                    data: $('#formInput').serialize(),
                    success: function (result) {
                        if (result == 'sukses') {
                            Swal.fire(
                                'Sukses!',
                                'Data berhasil ditambahkan',
                                'success'
                            ).then((result) => {
                                location.reload()
                            })
                        }
                        else {
                            Swal.fire(
                                'Error!',
                                '' + result,
                                'error'
                            )
                        }
                    }
                })
                   
                
            }
        }
    })
})

function Add() {
    $('#modalForm').modal()
    $('#updateform').hide()
    $('#AddNew').show()
    $('.fg-line').removeClass('fg-toggled')
    $('.modal-title').html('Add New Labour Rates')  
}
let baseUrl = localStorage.getItem('thisAddress')
$(function () {
    $('#TablesData').bootgrid({
        caseSensitive: false,
        formatters: {
            'commands': function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"PushUpdate(\'" + row.RateNo + "\',\'" + row.RateName + "\')\"><span class=\"zmdi zmdi-edit\"></span></button>" +
                    "<button type=\"button\" class=\"btn btn-icon command-delete waves-effect waves-circle\" onclick=\"PushDelete(\'" + row.RateNo + "\',\'" + row.RateName + "\')\"><span class=\"zmdi zmdi-delete\"></span></button>"
            },
            'Price': function (column, row) {
                return `Rp. ${parseFloat(row.Price).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'SetupCost': function (column, row) {
                return `Rp. ${parseFloat(row.SetupCost).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'MaintenanceCost': function (column, row) {
                return `Rp. ${parseFloat(row.MaintenanceCost).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'Salvage': function (column, row) {
                return `Rp. ${parseFloat(row.Salvage).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'Age': function (column, row) {
                return `${row.AgeUsed} Month`
            },
            'Capacity': function (column, row) {
                return `${row.Capacity} Liters`
            }
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
            $.ajax({
                type: 'POST',
                url: baseUrl + '/HPPItem/RatePOST',
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
    })
})

function Add() {
    $('#modalForm').modal()
    $('#updateform').hide()
    $('#AddNew').show()
    $('.fg-line').removeClass('fg-toggled')
    $('.cost-line').addClass('fg-toggled')
    $('.modal-title').html('Add New Tank Rates')
}
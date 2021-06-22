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
                return `Rp. ${parseFloat(row.Price).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}`
            },
            'Tax': function (column, row){
                return `Rp. ${parseFloat(row.Tax).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}`
            },
            'MTCost': function (column, row) {
                return `Rp. ${parseFloat(row.MaintenanceCost).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}`
            },
            'Insurance': function (column, row) {
                return `Rp. ${parseFloat(row.Insurance).toLocaleString('en-US', { minimumFractionDigits:2, maximumFractionDigits:2 })}`
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
                },
                error: function (jqXHR, exception) {
                    Swal.fire(
                        'Error',
                        'error ' + jqXHR.status,
                        'error'
                    )
                }
            })
        }
    })

    //Simpan form Update
    $('#updateform').click(function () {
        if ($('#formInput').valid()) {
            $.ajax({
                type: 'PUT',
                url: baseUrl + '/HPPItem/RatesUpdate',
                data: $('#formInput').serialize(),
                success: function (result) {
                    if (result == 'sukses') {
                        Swal.fire(
                            'Sukses!',
                            'Data berhasil diubah',
                            'success'
                        ).then((result) => {
                            location.reload()
                        })
                    } else {
                        Swal.fire(
                            'Error!',
                            '' + result,
                            'error'
                        )
                    }
                },
                error: function (jqXHR, exception) {
                    Swal.fire(
                        'Error',
                        'error ' + jqXHR.status,
                        'error'
                    )
                }
            })
            
        }
    })
})

function Add() {
    $('#modalForm').modal()
    $('#updateform').hide()
    $('#AddNew').show()
    $('.fg-line').addClass('fg-toggled')
    $('.rm-toggled').removeClass('fg-toggled')
    $('.modal-title').html('Add New Gedung & Kendaraan Rate')
}

function PushUpdate(No, Name) {
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPItem/SingleRates?No=' + No,
        success: function (result) {
            if (result != false) {
                /*Declare Modal*/
                $('#modalForm').modal()
                $('#updateform').show()
                $('#AddNew').hide()
                $('.modal-title').html(`Edit - ${Name} Rates`)

                $('#RateNo').val(result.rateNo)
                $('#RateName').val(result.rateName)
                $('#Price').val(result.price)
                $('#Tax').val(result.tax)
                $('#MaintenancePrice').val(result.maintenancePrice)
                $('#AgeUsedMonth').val(result.ageUsedMonth)
                $('#Insurance').val(result.insurance)
                $('.fg-line').addClass('fg-toggled')
            }
        },
        error: function (jqXHR, exception) {
            Swal.fire(
                'Error',
                `${exception} ${jqXHR.status}`,
                'error'
            )
        }
    })
}
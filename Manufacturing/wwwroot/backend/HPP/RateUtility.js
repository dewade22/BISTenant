let baseUrl = localStorage.getItem('thisAddress')
$(function () {
    $('#TablesData').bootgrid({
        caseSensitive: false,
        formatters: {
            'commands': function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"PushUpdate(\'" + row.RateNo + "\',\'" + row.RateName + "\')\"><span class=\"zmdi zmdi-edit\"></span></button>" +
                    "<button type=\"button\" class=\"btn btn-icon command-delete waves-effect waves-circle\" onclick=\"PushDelete(\'" + row.RateNo + "\',\'" + row.RateName + "\')\"><span class=\"zmdi zmdi-delete\"></span></button>"
            },
            'rateSetup': function (column, row) {
                return `Rp. ${parseFloat(row.Pemasangan).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'rateRegular': function (column, row) {
                return `Rp. ${parseFloat(row.Regular).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'ratePeakHour': function (column, row) {
                return `Rp. ${parseFloat(row.Peak).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
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

    //Add Button
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

    //Update Button
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
                }
            })
        }
    })
    
})

function Add() {
    $('#modalForm').modal()
    $('#updateform').hide()
    $('#AddNew').show()
    $('.modal-title').html('Add New Utility Rates')
}
function PushUpdate(No, Name) {
    $('#modalForm').modal()
    $('#updateform').show()
    $('#AddNew').hide()
    $('.modal-title').html(`Edit - ${Name} Rates`)
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPItem/SingleRates?No=' + No,
        success: function (result) {
            if (result != false) {
                $('#RateNo').val(result.rateNo)
                $('#RateName').val(result.rateName)
                $('#SetupPrice').val(result.setupPrice)
                $('#RegularRate').val(result.regularRate)
                $('#PeakHourRate').val(result.peakHourRate)
                $('.fg-line').addClass('fg-toggled')
            }
        }
    })
}
function PushDelete(No, Name) {
    Swal.fire({
        title: 'Warning!',
        text: 'Hapus Rate untuk ' + Name,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya!',
        cancelButtonText: 'Kembali'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'PUT',
                url: baseUrl + '/HPPItem/HideRates?No=' + No,
                success: function (result) {
                    if (result == 'sukses') {
                        Swal.fire(
                            'Sukses',
                            'Rate ' + Name + ' Telah dihapus',
                            'success'
                        ).then((result) => {
                            location.reload()
                        })
                    } else {
                        Swal.fire(
                            'Error',
                            '' + result,
                            'error'
                        )
                    }
                }
            })
        }
    })
}
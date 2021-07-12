let baseurl = localStorage.getItem('thisAddress');

let processID = '';
$(function () {

    //Get Header untuk menampilkan form add terlebih dahulu
    $.ajax({
        type: 'GET',
        url: baseurl + '/HPPHelper/ModelProcessHeader?ModelId=' + $('#BomId').val(),
        success: function (result) {
            if (result == 0) {
                $('#card-header').show()
                $('#card-line').hide()
            } else {
                $('#card-header').hide()
                $('#card-line').show()
            }
        },
        error: function (jqXHR) {
            Swal.fire(
                'Error!',
                'Error ' + jqXHR.status,
                'error'
            )
        }
    })

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
    //Validasi form header
    //Validasi
    $('#formHeader').validate({
        onkeyup: function (element) { $(element).valid() }
    })
    //Save Header
    $('#btnAddHeader').click(function () {
        Swal.fire({
            title: 'Save ?',
            text: 'Save Process Size?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya!',
            cancelButtonText: 'Kembali'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: baseurl + '/HPPHelper/SaveDetailProcessHeader',
                    data: $('#formHeader').serialize(),
                    success: function (result) {
                        console.log(result)
                        if (result == 'sukses') {
                            Swal.fire(
                                'Sukses',
                                'Data Tersimpan',
                                'success'
                            ).then((result) => {
                                window.location.reload()
                            })
                        } else {
                            Swal.fire(
                                'Error !',
                                'Error ' + result,
                                'error'
                            )
                        }
                    },
                    error: function (jqXHR) {
                        Swal.fire(
                            'Error !!',
                            'Error ' + jqXHR.status,
                            'error'
                        )
                    }
                })
            }
        })
    })
})

//Modal Untuk menambahkan mixing size
$('#AddMixingSize').click(function() {
    $('#modalHeader').modal()
    $('.modal-title').html(`Add Mixing Size`)
})
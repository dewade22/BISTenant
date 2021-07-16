let baseurl = localStorage.getItem('thisAddress');

$(function () {
    $('#tableItem').bootgrid({
        caseSensitive: false,
        formatters: {
            'Action': function (column, row) {
                return `${row.Type == 'electricity' ? `` : `<button type="button" class="btn btn-icon command-edit waves-effect waves-circle" onclick="PushUpdate('${ row.Id } ', '${ row.ItemDescription } ')"><span class="zmdi zmdi-edit"></span></button>` +
                    `<button type="button" class="btn btn-icon command-delete waves-effect" onclick="PushDelete('${row.Id}', '${row.ItemDescription}')"><span class="zmdi zmdi-delete"></span></button>`}`
            },
            'Qty': function (column, row) {
                return `${parseFloat(row.Qty).toFixed(4)}`
            },
            'ProcessHour': function (column, row) {
                return `${parseFloat(row.ProcessHour).toFixed(4)} Hour`
            },
            'ItemCost': function (column, row) {
                return `Rp. ${parseFloat(row.ItemCost).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}`
            },
            'Amount': function (column, row) {
                return `Rp. ${row.Type == 'Labour' ? parseFloat((row.Qty) * (row.ProcessHour) * (row.ItemCost)).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : parseFloat((row.Qty) * (row.ItemCost)).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            }
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

    //Button untuk sinkron FOH ke penyusutan
    $('#AddUpdateFOH').click(function () {
        Swal.fire({
            title: 'Add or Update Penyusutan FOH?',
            text: 'Anda akan melakukan sinkronisasi FOH ke COST untuk penyusutan!',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya!',
            cancelButtonText: 'Kembali'
        }).then((hasil) => {
            if (hasil.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: baseurl + '/HPPHelper/SyncronFOH?Model=' + $('#BomId').val() + '&Header=' + $('#txtHeaderId').val()+'&SPID=SUB-00002',
                    success: function (result) {
                        if (result == 'sukses') {
                            Swal.fire(
                                'Sukses',
                                'Sinkronisasi berhasil',
                                'success'
                            ).then((hasil) => {
                                window.location.reload()
                            })
                        } else {
                            Swal.fire(
                                'Error!',
                                'Error ' +result,
                                'error'
                            )
                        }
                    },
                    error: function (jqXHR) {
                        Swal.fire(
                            'Error',
                            'Error ' + jqXHR.status,
                            'error'
                        )
                    }
                })
            }
        })
    })

    //Add Item button click
    $('#AddItem').click(function () {
        $('#modalItem').modal()
        $('#updateform').hide()
        $('#AddNew').show()
        $('.modal-title').html('Add New Items')
    })
})
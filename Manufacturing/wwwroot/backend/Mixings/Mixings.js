let baseUrl = localStorage.getItem('thisAddress');

//Push global variabel
let ItemUnitCost;


$(function () {
    $('#sbRateLabour').hide()
    $('#tableItem').bootgrid({
        caseSensitive: false,
        formatters: {
            'Action': function (column, row) {
                return `${row.Type == 'electricity' ? `` : `<button type="button" class="btn btn-icon command-edit waves-effect waves-circle" onclick="PushUpdate('${row.Id} ', '${row.Type}', '${row.ItemDescription}')"><span class="zmdi zmdi-edit"></span></button>` +
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
                    url: baseUrl + '/HPPHelper/SyncronFOH?Model=' + $('#BomId').val() + '&Header=' + $('#txtHeaderId').val()+'&SPID=SUB-00002',
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

    //selectbox Type change function
    $('#Type').change(function () {
        if ($('#Type').val() != null || $('#Type').val() != '') {
            $('#itemname').html($('#Type option:selected').text())
            AppendItemNo($('#Type').val())
            if ($('#Type').val() == 'Labour') {
                $('#sbRateLabour').show()
            } else {
                $('#sbRateLabour').hide()
            }
        }
    })

    //Select Box ItemNo Change, get Text into Item Desc
    $('#ItemNo').change(function () {
        if ($('#ItemNo').val() != null || $('#ItemNo').val() != '') {
            $('#ItemDescription').val($('#ItemNo option:selected').text())
            GetCostofItem($(this).val(), $('#LabourRate').val())
        }
    })

    //Rate Labour Type Change
    $('#LabourRate').change(function () {
        if (($('#ItemNo').val() != null || $('#ItemNo').val() != '') && ($('#ItemQty').val() != null || $('#ItemQty').val() != '')) {
            GetCostofItem($('#ItemNo').val(), $(this).val())
        }
    })

    //Save Process
    $('#AddNew').click(function () {
        if ($('#formInput').valid()) {
            $('#ErrorSelect1').html('')
            $('#ErrorSelect2').html('')
            if ($('#Type').val() == null || $('#Type').val() == '') {
                $('#ErrorSelect1').html('Please Choose One!')
                $('#ErrorSelect2').html('Please Choose One!')
            } else if ($('#ItemNo').val() == null || $('#ItemNo').val() == '') {
                $('#ErrorSelect2').html('Please Choose One!')
            } else {
                Swal.fire({
                    title: 'Save ?!',
                    text: 'Simpan Record ini? ',
                    icon: 'question',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Ya!',
                    cancelButtonText: 'Kembali'
                }).then((jawab) => {
                    if (jawab.isConfirmed) {
                        $.ajax({
                            type: 'POST',
                            url: baseUrl + '/HPPHelper/SaveMixing',
                            data: $('#formInput').serialize(),
                            success: function (result) {
                                if (result == 'sukses') {
                                    Swal.fire(
                                        'Sukses',
                                        'Data Berhasil Ditambahkan',
                                        'success',
                                    ).then((hasil) => {
                                        window.location.reload()
                                    })
                                } else {
                                    Swal.fire(
                                        'Error !!',
                                        '' + result,
                                        'error'
                                    )
                                }
                            },
                            error: function (jqXHR) {
                                Swal.fire(
                                    'Error !',
                                    'Error ' + jqXHR.status,
                                    'error'
                                )
                            }
                        })
                    }
                })
            }
        }
    })

    //On Hiden Modal
    $('#modalItem').on('hidden.bs.modal', function () {
        $('#modalItem form')[0].reset()
        $('.chosen').val('')
        $('.chosen').trigger('chosen:updated')

    })
})

function AppendItemNo(ItemType, ItemNo = '') {
    $('#ItemNo').empty()
    $('#ItemNo').append(`<option value="">Choose One</option>`)
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPHelper/SelectItem?No=' + ItemType,
        success: function (result) {
            result.forEach(function (result) {
                $('#ItemNo').append(`<option value="${result.valueCode}">${result.valueName}</option>`)
            })
            $('#ItemNo').val(ItemNo)
            $('.chosen').trigger('chosen:updated')
        },
        error: function (jqXHR, exception) {
            Swal.fire(
                'Error !!',
                'Error ' + jqXHR.status,
                'error'
            )
        }
    })
}

function GetCostofItem(No, Labour) {
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPHelper/SpecificItem?Type=' + $('#Type').val() + '&NO=' + No + '&Labour=' + Labour,
        success: function (result) {
            ItemUnitCost = result.unitCost
            $('#satuanQty').html(result.unitofMeasure)
            $('#ItemCost').val(ItemUnitCost).val()
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

function PushUpdate(Id, Type, Name) {
    if (Type == 'Labour') {
        $('#sbRateLabour').show()
    } else {
        $('#sbRateLabour').hide()
    }
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPHelper/GetMixingLine?Id=' + Id,
        success: function (result) {
            if (result.status == 0) {
                Swal.fire(
                    'Error !!',
                    result.result,
                    'error'
                )
            } else {
                let res = result.result
                $('#modalItem').modal()
                $('#updateform').show()
                $('#AddNew').hide()
                $('.modal-title').html(`Edit - ${Name}`)

                AppendItemNo(res.type, res.itemNo)

                $('#ModelId').val(res.modelId)
                $('#Type').val(res.type)
                $('#SubProcessId').val(res.subProcessId)
                $('#ProcessHeaderNo').val(res.processHeaderNo)
                $('#Description').val(res.description)
                $('#ItemQty').val(res.itemQty)
                $('#ItemCost').val(res.itemCost)
                $('#ProcessHour').val(res.processHour)
                $('.chosen').trigger('chosen:updated')
                $('.fg-line').addClass('fg-toggled')
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
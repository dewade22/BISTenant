let baseUrl = localStorage.getItem('thisAddress');

let dataTables = $('#tableValuation').DataTable({
    "scrollX": true,
    "autoWidth": true,
    "columnDefs": [
        {
            "render": function (data, type, row) {
                return data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            },
            "targets": [3,4,5,7,8,9,11,12,13,15,16,17]
        },
        {
            "render": function (data, type, row) {
                let result = row[5] == 0 ? 0 : row[3] == 0 ? 0 : row[5]/row[3]
                return result.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            },
            "targets": 6
        },
        {
            "render": function (data, type, row) {
                let result = row[9] == 0 ? 0 : row[7] == 0 ? 0 : row[9] / row[7]
                return result.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            },
            "targets": 10
        },
        {
            "render": function (data, type, row) {
                let result = row[13] == 0 ? 0 : row[11] == 0 ? 0 : row[13] / row[11]
                return result.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            },
            "targets": 14
        },
        {
            "render": function (data, type, row) {
                let result = row[17] == 0 ? 0 : row[15] == 0 ? 0 : row[17] / row[15]
                return result.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            },
            "targets": 18
        }
    ]
});
$('#search').click(function () {
    jQuery('#tableValuation tbody').empty();
    doSomething($('#startDate').val(), $('#endDate').val(), $('#invenRpt').val(), $('#cboCategory').val(), $('#cboProductGroups').val(), $('#cboLocation').val(), $('#cboFlavour').val(), $('#cboSize').val())
})

function doSomething(startingDate = '', endingDate = '', invenRPT = '', category = '', prodGroups = '', location = '', Flavour = '', Size = '') {
    
    if (startingDate == '')
    {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Start Date not Define !!',
        })
    }
    else if (endingDate == '')
    {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'End Date not Define !!',
        })

    }
    else
    {
        let startDate = convertDate(startingDate);
        let endDate = convertDate(endingDate);
        if (new Date(startDate).getTime() >= new Date(endDate).getTime()) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Start Date Greater Then End Date !!',
            })
        }
        else {
            dataTables.clear();
            //Ajax
            $.ajax({
                type: 'GET',
                data: {
                    startDate: startDate,
                    endDate: endDate,
                    invenRPT: invenRPT,
                    category: category,
                    prodGroups: prodGroups,
                    location: location,
                    Flavour: Flavour,
                    Size: Size
                },
                url: baseUrl + '/Inventory/ValuationData',
                contentType: 'application/json',
                success: function (result) {
                    result.forEach(function (result) {
                        dataTables.row.add([
                            result.inventoryPostingGroup,
                            result.itemCategoryCode,
                            result.description,
                            result.qtyStarting,
                            result.literStarting,
                            result.valueStarting,
                            0,
                            result.qtyIn,
                            result.literIn,
                            result.valueIn,
                            0,
                            result.qtyOut,
                            result.literOut,
                            result.valueOut,
                            0,
                            result.qtyEnding,
                            result.literEnding,
                            result.valueEnding,
                            0
                        ])
                    })
                    dataTables.draw();
                }
            })
        }
       
    }
}
function convertDate(dates) {
    let parseDate = dates.split('/');
    let myDate = parseDate[1] + '-' + parseDate[0] + '-' + parseDate[2]
    return myDate;
}

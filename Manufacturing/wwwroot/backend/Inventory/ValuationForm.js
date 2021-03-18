
let baseUrl = localStorage.getItem('thisAddress');
$('#search').click(function () {
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
                        console.log(result.itemNo)
                    })
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
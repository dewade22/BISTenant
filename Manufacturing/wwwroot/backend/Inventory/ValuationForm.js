$('#search').click(function(){
    doSomething($('#startDate').val(), $('#endDate').val(), $('#invenRpt').val(), $('#cboCategory').val(), $('#cboProductGroups').val(), $('#cboLocation').val(), $('#cboFlavour').val(), $('#cboSize').val())
})

function doSomething(startDate = '', endDate = '', invenRPT = '', category = '', prodGroups = '', location = '', Flavour = '', Size = '') {
    if (startDate == '')
    {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Start Date not Define !!',
        })
    }
    else if (endDate == '')
    {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'End Date not Define !!',
        })

    }
    else
    {
        //Ajax
        $.ajax({
            type: 'GET',
            data: {
                    startDate : startDate,
                    endDate : endDate,
                    invenRPT : invenRPT,
                    category : category,
                    prodGroups : prodGroups,
                    location : location,
                    Flavour : Flavour,
                    Size : Size
                  },
            url: '/Inventory/ValuationData',
            contentType: 'application/json',
            succes: function (result) {
                console.log(result);
            }
        })
    }
}

let dates = new Date();
let month = dates.getMonth()+1;
let year = dates.getFullYear();
let day = dates.getDate();
let fullDT = month + '-' + day + '-' + year;
let baseurl = window.location.origin;

Days();
function Days(date = '03-03-2020') { 
    
    $.ajax({
        type: 'GET',
        dataType: 'json',
        data: {
            DateTime: date
        },
        url:  baseurl+ '/BalimoonBMI/SalesActual/BMIDaysMonth',
        success: function (res) {
            $('#transactionDate').html(date);
            $('#noDays').html(res.noDaysMonth);
            $('#totalDays').html(res.totalDayMonth);
        },
        
    });
};

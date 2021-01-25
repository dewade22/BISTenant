$(function () {
    //init DataTable
    $('#tableVendors').DataTable({
        processing: true,
        serverSide: true,
        //Paging Setup
        paging: true,
        ajax: {
            url: localStorage.getItem('thisAddress')+'/Vendors/VendorList',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: function (d) {
                return JSON.stringify(d);
                
            }
        },
        columns:
            [
                { data: 'VendorId', isIdentity: true, visible: false },
                { data: 'VendorNo' },
                { data: 'VendorName' },
                { data: 'VendorAddress' },
                { data: 'VendorContact' },
                {
                    render: function (data, type, full, meta) {
                        return "<a href='#' class='btn btn-outline-info'><span class='zmdi zmdi-eye'></span></a>";
                    }
                }
            ]
    })
})
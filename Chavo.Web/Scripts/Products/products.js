$("#CategoryId").change(function () {
    $("#SubCategoryId").empty();
    $.ajax({
        type: 'POST',
        url: '/Products/GetSubCategories',
        dataType: 'json',
        data: { id: $("#CategoryId").val() },
        success: function (items) {
            $.each(items, function (i, item) {
                $("#SubCategoryId").append('<option value="'
                    + item.SubCategoryId + '">'
                    + item.Name + '</option>');
            });
        },
        error: function (ex) {
            alert.alert('Alert', 'Error al mostrar los SubCategory.' + ex);
        }
    });
}); 
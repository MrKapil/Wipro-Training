$(document).ready(function(){
    $(".deleteGenreBtn").click(function(){
        if(confirm("Are you sure to delete this genre?")){
            var id = $(this).data("id");
            $.ajax({
                url: '/Genre/Delete/' + id,
                type: 'POST',
                success: function(response){
                    if(response.success){
                        location.reload();
                    } else {
                        alert("Error deleting genre");
                    }
                }
            });
        }
    });
});

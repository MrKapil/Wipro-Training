$(document).ready(function(){
    $(".deleteAuthorBtn").click(function(){
        if(confirm("Are you sure to delete this author?")){
            var id = $(this).data("id");
            $.ajax({
                url: '/Author/Delete/' + id,
                type: 'POST',
                success: function(response){
                    if(response.success){
                        location.reload();
                    } else {
                        alert("Error deleting author");
                    }
                }
            });
        }
    });
});

$.ready(function(){
    $('#test').click(function () {
        $.post( '/api/bortamatch/nyttInlägg', $('#testForm').serialize(), function(data) {
        },
           'json' // I expect a JSON response
        );
    });
});
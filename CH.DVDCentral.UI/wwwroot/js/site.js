// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

<script>
    var loadfile = function (event) {
        var output = document.getElementById('movieimage');
    output.src = URL.createObjectURL(event.target.files[0]);
    }
</script>

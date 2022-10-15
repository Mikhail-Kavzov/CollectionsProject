$('#input__file').change(function(){
    if (!this.files)
    return;
    $('#image-photo').attr('src',URL.createObjectURL(this.files[0]));
});


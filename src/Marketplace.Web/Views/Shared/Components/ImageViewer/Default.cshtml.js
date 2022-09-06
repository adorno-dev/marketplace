const imageViewerComponent = document.querySelector(".image-viewer");

function ImageViewerInit()
{
    window.onclick = (event) => {
        if (event.target.contains(imageViewerComponent))
            imageViewerComponent.classList.remove("active");
    }    
}

imageViewerComponent && ImageViewerInit();
const imageBrowserComponent = document.querySelector(".image-browser");

function ImageBrowserInit()
{
    const imageViewer = document.querySelector(".image-viewer");
    const inputFiles = document.querySelector("#images");

    inputFiles.addEventListener("change", (e) =>
    {
        const files = e.srcElement.files;

        imageBrowserComponent.querySelectorAll("img")
                    .forEach(img => imageBrowserComponent.removeChild(img));

        for (let i = 0; i < files.length; i++)
        {
            let read = new FileReader();
            let file = files[i];
            let view = document.createElement("img");

            view.setAttribute("data-file", file.name);
            imageBrowserComponent.appendChild(view);
            read.onloadend = () => view.src = read.result;
            read.readAsDataURL(file);

            view.onclick = (e) => {
                imageViewer.classList.add("active");
                imageViewer.querySelector("img").src = e.target.src;
            }
        }
    });

    imageBrowserComponent.querySelectorAll("img")
                .forEach(f => f.addEventListener("click", (e) =>
                {
                    imageViewer.classList.add("active");
                    imageViewer.querySelector("img").src = e.target.src;
                }));
}

imageBrowserComponent && ImageBrowserInit();
const selectComponent = document.querySelector(".select");

function SelectInit()
{
    const input = selectComponent.querySelector("input");
    const label = selectComponent.querySelector("label");
    const list = selectComponent.querySelector("ul");
    const items = list.querySelectorAll("li");

    selectComponent.addEventListener("click", (event) => {
        event.stopPropagation();
        if (selectComponent === null)
            return;
        document.querySelectorAll(".active").forEach(control => {
            if (!control.contains(selectComponent))
                control.classList.remove("active");
        })
        window.onclick = () => {
            window.onclick = null;
            selectComponent.classList.remove("active");
        }
        selectComponent.classList.toggle("active");
    })

    items.forEach(item => {
        item.addEventListener("click", () => {
            let value = item.getAttribute("value");
            list.querySelectorAll("li.selected").forEach(selected => selected.classList.remove("selected"));
            if (input === undefined || label === null)
                return;
            if (value !== null) {
                label.innerText = item.innerText;
                item.className = "selected";
                input.value = value;
            } else {
                label.innerText = "@Html.Raw(@Model.text)";
                input.value = "";
            }
        })
    })
}

selectComponent && SelectInit();
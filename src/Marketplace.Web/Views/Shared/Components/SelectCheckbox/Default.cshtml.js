const selectCheckboxComponent = document.querySelector(".select-checkbox");

function SelectCheckboxInit()
{
    const label = selectCheckboxComponent.querySelector("label");
    const list = selectCheckboxComponent.querySelector("ul");
    const items = list.querySelectorAll("li");

    selectCheckboxComponent.addEventListener("click", (event) => {
        event.stopPropagation();
        document.querySelectorAll(".active").forEach(activeControl => {
            if (!activeControl.contains(selectCheckboxComponent))
                activeControl.classList.remove("active");
        });
        window.onclick = () => {
            window.onclick = null;
            selectCheckboxComponent.classList.remove("active");
        }
        selectCheckboxComponent.classList.toggle("active");
    });

    items.forEach(item => {
        item.addEventListener("click", (event) => {
            let values = list.querySelectorAll("input[type=checkbox]:checked");
            if (label !== null && values !== undefined) {
                label.innerText = values.length > 0 ?
                    `${label.getAttribute("data-text")} (${values.length})`:
                    `${label.getAttribute("data-text")}`;
            }
        })
    })


    label.innerText = `${label.getAttribute("data-text")} (${label.getAttribute("data-values")})`;
}

selectCheckboxComponent && SelectCheckboxInit();
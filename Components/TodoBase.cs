using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TodoApp.Objects;
using Blazored.LocalStorage;

namespace TodoApp.Components
{
    public partial class TodoBase : ComponentBase
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; } = default!;

        public string key = "items";
        public List<Item>? items = new List<Item>();
        public Item? editingItem;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //TODO: Show little loading animation
                await LoadItems();
            }
        }

        private async Task LoadItems()
        {
            items = await LocalStorage.GetItemAsync<List<Item>>(key) ?? new List<Item>();
            StateHasChanged();
        }

        protected async Task StoreItems()
        {
            await LocalStorage.SetItemAsync<List<Item>>(key, items!);
        }

        protected async Task CreateItem(Item newItem)
        {
            if (items == null)
            {
                items = new List<Item>();
            }
            items!.Add(newItem);
            await StoreItems();
        }

        protected async Task RemoveItem(Item item)
        {
            items!.Remove(item);
            await StoreItems();
            StateHasChanged();
        }

        protected void EditItem(Item item)
        {
            editingItem = item;
        }

        protected async Task SaveEdit(Item item)
        {
            editingItem = null;
            await StoreItems();
        }

        protected async void CheckForEnter(KeyboardEventArgs e, Item item)
        {
            if ((e.Key == "Enter" || e.Key == "NumpadEnter"))
            {
                await SaveEdit(item);
            }
        }
    }
}

# Unity UI Popup System

A lightweight, modular **popup system** for Unity


---

## Features
- Lightweight & easy to integrate
- Supports **multiple buttons**
- Supports **async and sync** button callbacks
- Handles popup queueing automatically

---

## Installation


### Clone or Download from GitHub
1. Clone or download this repository:
   ```bash
   git clone https://github.com/Muhammadkhodaverdi/Unity-UI-Popup-System.git
   ```
2. Copy the **`PopupSystem/`** folder into your project’s **`Assets/`** folder.
3. Done 

---

## API Overview

### `PopupBuilder`
| Method | Description |
|--------|-------------|
| `Create(string title)` | Creates a popup with a title |
| `SetMessage(string message)` | Sets the popup body text |
| `AddButton(string label, Action callback)` | Adds a synchronous button |
| `AddAsyncButton(string label, Func<Task> callback)` | Adds an asynchronous button |
| `Show()` | Displays the popup |

---

## License
This project is open-source and available under the [MIT License](LICENSE).

---

## 👤 Author
**M. Khodaverdi**  
[GitHub Profile](https://github.com/Muhammadkhodaverdi)

---

Enjoy building better popups

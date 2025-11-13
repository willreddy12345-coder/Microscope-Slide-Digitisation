# EPQ: Microscope Slide Image Database

This project is a C# Windows Forms application designed to browse, search, and display a collection of microscope slide images. It functions as a simple database viewer, loading image metadata (tags) from an external CSV file and allowing the user to filter and navigate the collection.

This was built as an "Extended Project Qualification" (EPQ).



## Core Features

* **Image Database:** Loads a collection of images (e.g., `0.png`, `1.png`) and their corresponding metadata from a `test.csv` file.
* **Tag-Based Search:** Users can search for images using a search bar. The application filters the collection to show only images that have a tag *starting with* the search term.
    * Searching for `*` will reset the view and show all images.
* **Dual View Mode:**
    * **Single View:** Displays one large image at a time.
    * **Grid View:** Displays an 8-image grid (2x4) for quick browsing.
    * Users can toggle between these modes with the "Switch Display" button.
* **Navigation:**
    * "Forward" and "Backward" buttons allow for easy navigation.
    * In Single View, it moves one image at a time.
    * In Grid View, it moves one *page* (8 images) at a time.
* **Show Info:** An "Info" button toggles a text overlay that displays all the tags associated with the currently selected image(s).
* **Custom UI:** The application uses custom-drawn images for buttons and a frame, creating a unique visual theme.

## How to Set Up & Run

This project **requires external files** (a CSV file and the images) to be placed in the execution directory to function correctly.

1.  **Clone the Repository:**
    ```bash
    git clone <your-repo-url>
    ```
2.  **Open in Visual Studio:**
    Open the `EPQ.sln` file (or `EPQ.csproj`) in Visual Studio.
3.  **Create Data Files:**
    Navigate to the project's execution directory. This is typically:
    `.../EPQ/bin/Debug/`
4.  **Add `test.csv`:**
    Inside the `bin/Debug` folder, create a new file named `test.csv`. This file must contain the tags for your images, separated by commas. Each line corresponds to an image (line 1 = `0.png`, line 2 = `1.png`, etc.).

    *Example `test.csv` content:*
    ```csv
    animal,cell,invertebrate
    plant,leaf,stomata
    bacteria,micro,organism
    ...
    ```
5.  **Add Image Files:**
    Place all your slide images in the **same `bin/Debug` folder**. The application expects them to be named sequentially in `.png` format:
    * `0.png` (matches line 1 of `test.csv`)
    * `1.png` (matches line 2 of `test.csv`)
    * `2.png`
    * ...
    * `noresults.png` (an image to show when no search results are found)

6.  **Add UI Images:**
    The application also loads its button graphics from this same folder. Make sure these files are present in `bin/Debug`:
    * `SingleToMultiple.png`
    * `MultipleToSingle.png`

7.  **Run the Project:**
    Build and run the project from Visual Studio. The application will load your images and tags from the CSV, and the browser will be fully functional.

## Tech Stack

* **Language:** C#
* **Framework:** .NET (Windows Forms)

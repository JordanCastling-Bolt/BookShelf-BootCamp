# BookShelf-BootCamp: Librarian Training Program

## Developer Information
- **Developer**: Jordan Castling-Bolt
- **Student ID**: ST10114132
- **Course**: BCAD Year 3 Group 1 : PROG7312 POE Part 2
- **Date**: 2023/10/26
- **Repo**: https://github.com/JordanCastling-Bolt/BookShelf-BootCamp

## Table of Contents
1. Installation
2. Usage
3. Features
4. References

---

### Installation

1. **Download and Extract Files**: 
    - Download the zip file and extract its contents.

2. **Run the Application**: 
    - Option 1: Navigate to `BookShelf-BootCamp\BookShelfBootCamp_Frontend\bin\Debug\` and double-click on `PROG7132.exe`.
    - Option 2: Double-click on the Visual Studio solution file located at `BookShelf-BootCamp\BookShelfBootCamp_Frontend\PROG7132.sln` and click the green "Start" arrow in the toolbar once Visual Studio opens.

---

### Usage

1. **Initial Screen**: 
    - Upon launching, the application will open to the 'Replacing books' feature by default.

2. **Generate Call Numbers**: 
    - Call numbers are generated on start up but users can click the 'Generate Call Numbers' button at the bottom of the List View to populate new call numbers.

3. **Reorder Books**: 
    - Select a book and drag it up or down the list to reorder the selection in ascending alphabetical or numerical order.
    - Note: The book needs to be clicked on first to be selected.

5. **Check Order**: 
    - Click the 'Check Order' button to validate the order. The program will indicate whether the re-order is correct or not at the bottom of the page.

4. **Match the Questions Feature**: 
    - Drag the correct Clue (left column) to the appropriate Match (right column). 
    - If the match is correct, both items will disappear from the list.
    - If the match is incorrect, the dragged item will turn red as an indication.
    - Complete 4 correct matches to complete the round.

6. **Progress Tracking**: 
    - A progress bar and label will update to show how many re-orders or matches have been correctly executed and how many are left to complete the progress bar.
    - Achievement badges unlock after 3, 5, 10 successful call number reorders or matches.

7. **Completion**: 
    - Upon successfully completing 10 correct re-orders or matches, a congratulatory message will appear, plus an achievement badge and the progress bar will reset.

---

### Features

- **Replacing Books**: Allows the user to practice organizing books by their call numbers.
- **Match the Questions**: Enables the user to match descriptions to the correct call numbers to reinforce their understanding.
- **Progress Tracking**: Keeps track of the user's progress in organizing the books and gives a badge for completing the progress bar.
- **Drag-and-Drop Interface**: Provides an intuitive way to reorder books or match descriptions.

---

### References 

- ChatGPT was used throughout the project.
- Sommerville, I. (2011). *Software Engineering: Principles and Practice* (9th ed.). Pearson Education Inc.
- Jamro, M. (2018). *C# Data Structures and Algorithms*. Packt Publishing.

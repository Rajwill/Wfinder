# Wfinder
Presented with a character matrix and a large stream of words, your task is to create a Class
that searches the matrix to look for the words from the word stream. Words may appear
horizontally, from left to right, or vertically, from top to bottom. In the example below, the word
stream has four words and the matrix contains only three of those words ("chill", "cold" and
"wind"):
Note: matrix size does not exceed 64x64,

# Implementation
I validate the matrix is neither null or empty. In each case I throw an exception. As part of the requirements I also validate the maxtrix doesn't exceed 64x64.
Strategy

The strategy I used for the Find method is the following:

    I iterate over the list of words within the wordstream.
    Then for each word I validate the word is not null, empty or a space and then iterate over the board.
    The iteration over the board it's with two for statement to optimize resources.
        I verify if the first character of the word to search it's equal to the character in the give possition.
        As well I call the method FindWithDirection with the right paramenters, if both conditions are true then I add that word to the list of results.
        The method FindWithDirection it's responsible to itereate over the board in order to find the rest of the word. It receives the current possition and the count of how many chars of the given word has found already. The method use it as base condition: Once the count it's equals to the length of the word to search it returns true. The method iterates recursively to find in all directions in case it's the first character of the word. once it picks a direction sticks with it to prevent find the word in a snake like way.

Running UnitTest

The solution has a UnitTest project in order to quickly validate the different use cases. by executing the unit test on the Class WFinderTest.cs you will be able to validate different use cases and validations.

  The idea is to validate the minimum viable product.

The solution has two different ways to execute. You can either select the console app or the rest api each of them has a different way to execute it.
Running the Console app

In order to execute the console app you need to select it first as startup project.

    Right click over the solution name
    Set Statup Projects
    Select the WFinder.ConsoleApp project.
    Press F5

A console application is going to prompt. You need to follow the instructions. You are going to be prompted to type the path to a file which is going to serve as grid matrix. Each line on that file will be a row in the matrix. You can have a maximum of 64 lines and each line should have a maximum of 64 characters.

    In the repo you will find two files FileToDemo1.txt and FileToDemo1.txt which are demo for the console app.

Once you select the file the application console is going to ask you for the words stream. You need to provide another file path but this time should have a single line that contains all the words to search separated with a blank space.

    If you provide more than one line the application is going to considere only the first one.

Running the Rest API

In order to execute the REST API you need to select it first as startup project.

    Right click over the solution name
    Set Statup Projects
    Select the WFinder.API project.
    Press F5

    Once the application started you need to use an external tool to comunicate with it. I recomend to use POSTMAN.

The REST API application has two endpoints:

    api/board/init
    api/board/find/{uid}

In the first method you need to provide a json array with the board. Same constrains as the application console. This will initialize the board on the API application. It will also return you an UiD that you wil need to use to call the second method.

If you call the second method before call the init the application it's going to response an 204 http response code.

The second method you need to post the words stream as a json array and using into the url the UiD you got in the previous step.

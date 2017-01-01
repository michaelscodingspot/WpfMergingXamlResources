1. This directory should contain XAML resource dictionaries.
2. These txaml files should be included in the solution. 
Do not rename them to .xaml or they will be compiled and embedded to your project.
3. An automatic tool included in this solution will merge these dictionaries into a single XAML file 
called AppXamlResources.xaml. Merge this file from App.xaml of your startup project as a merged dictionary
4. You can (and should) use sub-directories inside this folder.
Order does matter. Everything will be sorted alphabetically, recurssively.
So if you have a file called B.XAML with a resource x:Key="abcd" and a fle called C.XAML with another resource
that dependes on "abcd", everything will work. But if C.XAML were to be called A.XAML, it wouldn't work
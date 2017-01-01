1. This directory should contain XAML resource dictionaries.
2. These XAML files should NOT be included in the solution. Otherwise they will be compiled and embedded to your project.
3. An automatic tool included in this solution will merge these dictionaries into a single Generic.xaml file.
This should be used only for Default styles in this project.
4. You can (and should) use sub-directories inside this folder.
Order does matter. Everything will be sorted alphabetically, recurssively.
So if you have a file called B.XAML with a resource x:Key="abcd" and a fle called C.XAML with another resource
that dependes on "abcd", everything will work. But if C.XAML were to be called A.XAML, it wouldn't work
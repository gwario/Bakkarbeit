                       vutinfth LaTeX2e document class
                           Academic thesis template

             by Thomas Auzinger <thomas.auzinger@cg.tuwien.ac.at>


Description
-----------
The vutinfth document class is a LaTeX2e-based template for all theses written
at the Faculty of Informatics at the Vienna University of Technology. This
package includes the document class and its documentation as well as an example
document that includes a short introduction to LaTeX.


Support
-------
The vutinfth document class is developed using a GitLab-supported Git
repository. You can find it at
                https://gitlab.cg.tuwien.ac.at/auzinger/vutinfth
where a history of the development process is given. If you encounter problems
with using vutinfth, use the Issues section of GitLab to provide a description
and to alert the developers of your issue. Furthermore, you can use the Issues
section to request features or give suggestions.


Installation
------------
To use the vutinfth class, it has first to be generated from the documented
source file vutinfth.dtx. This is achieved by executing the build script for
Windows systems (build.bat) or Unix-based systems (build.sh). The generated
document class vutinfth.cls has then to be placed in a directory that is part
of the search path of the LaTeX compiler. The folder of the thesis file is a
valid location.
The build script also generate an exemplary LaTeX document that serves both as
an introduction to LaTeX and as a verification of the installed typesetting
system. The example document is compiled to example.pdf and it should match the
provided reference solution example-ref.pdf.


Documentation
-------------
The documentation can be found in vutinfth.pdf, which is generated directly
from vutinfth.dtx.


Files
-----
build.bat          The batch file to generate all necessary files on Windows
                     systems.
build.sh           The batch file to generate all necessary files on Unix-based
                     systems.
example-ref.pdf    The reference solution for the compiled example document.
example.tex        An LaTeX source file showing the use of the vutinfth
                     document class.
intro.bib          The bibliography file of intro.tex.
intro.tex          The LaTeX source file of a document that gives an
                     introduction to LaTeX.
lppl.txt           The LaTeX Project Pulic License v1.3c.
README.txt         This file.
vutinfth.dtx       The documented source code and documentation file of the
                     vutinfth document class.
vutinfth.ins       The installer file of the vutinfth document class.

graphics/*.*       Corporate identity graphics of the Vienna University of
                     Technology and its Faculty of Informatics.


Copyright Notice
----------------
Copyright (C) 2014 by Thomas Auzinger <thomas.auzinger@cg.tuwien.ac.at>

This work may be distributed and/or modified under the
conditions of the LaTeX Project Public License, either version 1.3
of this license or (at your option) any later version.
The latest version of this license is in
  http://www.latex-project.org/lppl.txt
and version 1.3 or later is part of all distributions of LaTeX
version 2005/12/01 or later.

This work has the LPPL maintenance status `maintained'.

The Current Maintainer of this work is Thomas Auzinger.

This work consists of the files vutinfth.dtx and vutinfth.ins
and the derived file vutinfth.cls.
This work also consists of the file intro.tex.

The 'TU Wien' logo and 'Fakultät für !nformatik' logo are copyright of the
Vienna University of Technology.
Thank you for your interest in contributing to this project.
This file contains some basic information about possible contributions.

- [DotGGPK repository][1]
- [DotGGPK issue tracker][2]
- [DotGGPK continuous integration][3]

# Issues
All issues can be found in the [project issue tracker][2].
When reporting new issues please make sure that you do not submit a duplicate and use the appropriate issue template to ensure providing as much information as possible:

- **Bug**: all kind of wrong software behavior should be reported using this issue template
- **Enhancement**: if there are any features that work as expected but could be improved please use this issue templates
- **Feature**: if there are any important features missing use this issue template to propose them
- **Question**: if you are not sure which issue template should be used or you just have a question about the software please use this template


# Code
If you want to contribute code please read the following rules:

## Workflow
Since you do not have write access to the repository a [project forking workflow][20] is used for external contributions:

0. Create a [fork of this project][21] into your own [GitLab workspace][22].
1. Create a feature branch based on the master branch, preferably with the issue number and a description in the name (e.g. `123-my-feature`).
2. Comment the issue you will work on and link your feature branch.
3. Submit a [merge request][23] to the `master` branch using the merge request template; make sure you completed the checklist in the template and mention the issue solved by this merge request to [auto-close][24] it when the branch has been merged. Use the [work in progress][25] indicator while working on the issue.

## Acceptance criteria
- Only submit merge requests for issues tagged as ~accepted
- Obey the StyleCop / CodeAnalysis rules configured in the project solution file
- Write unit tests with a reasonable code coverage
- Obey software architecture and design 

# Donations
If you like the software consider a [donation][10].

[1]: https://gitlab.com/tobiaskoch/DiabLaunch
[2]: https://gitlab.com/tobiaskoch/DiabLaunch/issues
[3]: https://gitlab.com/tobiaskoch/DiabLaunch/pipelines

[10]: https://www.tk-software.de/donate

[20]: https://docs.gitlab.com/ce/workflow/forking_workflow.html
[21]: https://docs.gitlab.com/ce/gitlab-basics/fork-project.html
[22]: https://about.gitlab.com/
[23]: https://docs.gitlab.com/ee/user/project/merge_requests/index.html
[24]: https://docs.gitlab.com/ee/user/project/issues/automatic_issue_closing.html
[25]: https://docs.gitlab.com/ce/user/project/merge_requests/work_in_progress_merge_requests.html
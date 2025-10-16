# The use of tags within feature files
Within this setup tags have a functional purpose. This might for example mean that a test might only be executed if a certain tag is present.

## Priority
All tests will be executed while running the full test suite.

### @smoke
This test will be executed on every environment, including PRD. This gives us a quick feedback if something is broken.
In this test it is important that the test is fast, does not require a lot of resources and **does not modify the state of the system**.
Test using this tag will also be executed while running the full test suite.

### @manual
This test will **only** be executed manually. It is not part of the full test suite.
An example would be a test which has a connection with an external source, which we only want to execute manually.

## Functional

### @cookies
When this tag is **not** present, cookies will be accepted automatically.
For tests containing this tag, cookies have to be accepted manually. This is useful for tests concerning the cookie acceptance popup.
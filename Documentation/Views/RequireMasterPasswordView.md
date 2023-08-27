# Require master password view

The contents of this view will depend of the state of an existing master password.

---
If it doesn't exist any master password yet, then
the system will ask the user to create a new one, 
it will consist of the following elements:
* A label called "**Master password**"
* A textbox that will contain the password
* Another label called "**Confirm master password**"
* And another textbox for the master password
* Finally, it will contain a button with the text "**Save**";

The "**Save**" button will activate, only if "**Master password**" 
and "**Confirm master password**" match.

The storage of the master password is still undefined.

---
Once master password is set, the system will require the user to write
the password to grant him access.

The view will consists of the following:
* A label called "**Master Password**".
* A textBox
* A button that will allow the user to access.

If the textbox is not filled, then the button will not activate.
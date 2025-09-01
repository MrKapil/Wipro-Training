import { ContactManager } from "./services/ContactManager";
import { Contact } from "./models/Contact";

const manager = new ContactManager();

// Adding contacts
manager.addContact({ id: 1, name: "Kapil Rathod", email: "kapil@example.com", phone: "1234567890" });
manager.addContact({ id: 2, name: "John Doe", email: "john@example.com", phone: "0987654321" });

// Viewing contacts
console.log("All Contacts:", manager.viewContacts());

// Modifying a contact
manager.modifyContact(1, { phone: "1112223333", email: "kapil.rathod@example.com" });
console.log("Contacts after modification:", manager.viewContacts());

// Deleting a contact
manager.deleteContact(2);
console.log("Contacts after deletion:", manager.viewContacts());

// Trying to modify/delete non-existing contact
manager.modifyContact(5, { name: "Test" });
manager.deleteContact(5);

import { Contact } from "../models/Contact";

export class ContactManager {
  private contacts: Contact[] = [];

  // Add contact
  addContact(contact: Contact): void {
    const exists = this.contacts.find(c => c.id === contact.id);
    if (exists) {
      console.log(`Error: Contact with id ${contact.id} already exists.`);
      return;
    }
    this.contacts.push(contact);
    console.log(`Contact with id ${contact.id} added successfully.`);
  }

  // View all contacts
  viewContacts(): Contact[] {
    if (this.contacts.length === 0) {
      console.log("No contacts found.");
    }
    return this.contacts;
  }

  // Modify contact
  modifyContact(id: number, updatedContact: Partial<Contact>): void {
    const contact = this.contacts.find(c => c.id === id);
    if (!contact) {
      console.log(`Error: Contact with id ${id} does not exist.`);
      return;
    }
    Object.assign(contact, updatedContact);
    console.log(`Contact with id ${id} updated successfully.`);
  }

  // Delete contact
  deleteContact(id: number): void {
    const index = this.contacts.findIndex(c => c.id === id);
    if (index === -1) {
      console.log(`Error: Contact with id ${id} does not exist.`);
      return;
    }
    this.contacts.splice(index, 1);
    console.log(`Contact with id ${id} deleted successfully.`);
  }
}

export interface Contact{
    id : string,
    firstName : string,
    lastName : string,
    email : string,
    dateOfBirth : Date | null,
    phone : number | null,
    password : string,
    category : string,
    subcategory : string
}
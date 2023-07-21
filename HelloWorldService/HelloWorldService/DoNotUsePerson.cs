using System.ComponentModel.DataAnnotations;

namespace HelloWorldService
{
//  {
//  "firstName": "John",
//  "lastName": "Smith",
//  "isAlive": true,
//  "age": 25,
//  "phoneNumbers": [
//    {
//      "type": "home",
//      "number": "212 555-1234"
//    },
//    {
//    "type": "office",
//      "number": "646 555-4567"
//    }
//  ],
//  "children": [],
//  "spouse": null
//  }

    // Define the C# class for the sample JSON above
    public class DoNotUsePerson
    {
        public string firstName;
        public string lastName;
        public bool isAlive;
        public int age;
        public DoNotUsePhone[] phoneNumbers;
        public DoNotUsePerson[] children;
        public DoNotUsePerson spouse;
    }

    public class DoNotUsePhone
    {
        public string type;
        public string number;

    }
}

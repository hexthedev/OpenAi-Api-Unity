# Context
To keep `OpenAi Api Unity` portable, it was important to reduce dependencies as much as possible. Making the library easy to use and light weight was a priority. 

As such, a simple, bare-minimum custom Json Serailizer/Deserializer was written. The serailizer shipped with `OpenAi Api Unity` is not intended for use outside of the library, and has only been tested and developed to handle JSON syntax present in OpenAi Api HTTP request/responses.

As more api calls are introduced, the Json Serailizer will be updated as needed. 

All custom Json code can be found in the `OpenAi.Json` namespace
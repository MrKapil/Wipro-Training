 class ReferenceExample
 {
     static ref int Find(int[] arr, int val) {

         // returning a reference to an element in the array
         return ref arr[Array.IndexOf(arr, val)];
     }

     static void Main()
     {
         int[] data = { 2, 3, 4 };
        ref int x =  ref Find(data,3);   // returning data[1]
         x = 34;

         Console.WriteLine(string.Join(",",data));
     }


 }
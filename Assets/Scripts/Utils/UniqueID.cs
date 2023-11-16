using System.Collections.Generic;
using System;
using System.Linq;

[System.Serializable]
public class UniqueID
{
    public string value = string.Empty;

    private static List<string> allocatedIDs = new List<string>();

    private static Random random;

    private int IDLenght = 8;

    public void InitialiseUniqueID()
    {
        random = new Random();
        value = RandomString(IDLenght);

        // Validity check
        foreach(string item in allocatedIDs) {
            if(value == item) {
                new UniqueID();
            }
        }
    }

    private string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

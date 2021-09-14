using System.Collections.Generic;
using System.Linq;

public interface IMultiLimb
{
    public string Name { get; set; }
    public int NumberOfLimbs { get; set; }
}

public class Animal : IMultiLimb
{
    public string Name { get; set; }
    public int NumberOfLimbs { get; set; }
}

public class Alien : IMultiLimb
{
    public string Name { get; set; }
    public int NumberOfLimbs { get; set; }
}

public class DTO<CreatureType> where CreatureType : IMultiLimb, new()
{
    //A propert which represents the model
    public List<CreatureType> OrderedList { get; set; }

    //Method that creates the above object using the response

    public void GetOrderedList(List<CreatureType> unorderedListOfCreatures)
    {
        OrderedList = (List<CreatureType>)unorderedListOfCreatures.OrderBy(c => c.NumberOfLimbs);
    }

}
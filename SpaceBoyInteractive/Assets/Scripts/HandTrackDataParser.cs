using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeomorphicGames
{
    public static class HandTrackDataParser 
    {
        public static List<Vector3> HandTracking(string rawData)
        {
            List<Vector3> result = new List<Vector3>(); 

            rawData = rawData.Trim();
            rawData = rawData.Replace("[", "").Replace("]", "");
            string[] splittedData = rawData.Split(",");

            for(int i = 0; i < splittedData.Length; i += 3)
            {
                Vector3 position = new Vector3(
                    ParseCoordinate(splittedData[i]),
                    ParseCoordinate(splittedData[i + 1]),
                    ParseCoordinate(splittedData[i + 2]));

                result.Add(position);
            }
            
            return result;  
        }

        private static float ParseCoordinate(string coordinate, bool print = false)
        {

            float result;   

            if (float.TryParse(coordinate,
            System.Globalization.NumberStyles.Float,
            System.Globalization.CultureInfo.InvariantCulture, out result))
            {
                if (print)
                {
                    Debug.Log("Raw Coordinate: " + coordinate);
                    Debug.Log("Parsed Coordinate: " + result);
                }
                return result;
            }

            result = 0;
            Debug.Log("Incorrect data. Received: " +  coordinate);
            
            return result;
        }

    }
}

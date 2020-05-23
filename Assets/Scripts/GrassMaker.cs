using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GrassMaker
{
    GameObject world;
    BitArray[] grassBitMap;
    GameObject grassBrand;

	public GrassMaker(GameObject grassBrand)
	{
        world = GameObject.FindGameObjectWithTag("World");

        grassBitMap = new BitArray[ 360 ];
        for (int y = 0; y < grassBitMap.Length; y++) grassBitMap[y] = new BitArray(180, false);

        this.grassBrand = grassBrand;
    }

    public void makeGrass(Vector3 fromPosition, int radius, int maximumHeigth) {
        int phi = (int)Math.Round(SphericalCoordinateHelper.getPhiFromCartesian(fromPosition.x, fromPosition.y));
        if (phi < 0)
            phi += 180;
        int theta = (int)Math.Round(SphericalCoordinateHelper.getThetaFromCartesian(fromPosition));
        if (theta < 0)
            phi += 90;
        int r = (int)Math.Round(SphericalCoordinateHelper.getRadiusFromCartesian(fromPosition));

        int phi2 = phi;
        int theta2 = theta;

        //Tant quon est à l'intérieur du rayon on fait pousser du gazon si il n'y en pas celon le bit map
        while (Vector3.Distance(SphericalCoordinateHelper.getVector3FromSpherical(theta, phi, r), fromPosition) < Mathf.Pow(radius, 2f)) {

            if (!grassBitMap[theta][phi]) {
                grassBitMap[theta][phi] = true;
                //Instancier le/les brins d'herbe dans la bonne orientation soit direction monde surface
                //Fonction normal absolue avec maximumHeight comme centre pour déterminer le nombre de brins d'hauteur(jespere que ca va donner un aspect de gazon)
                Vector3 strandPosition1 = SphericalCoordinateHelper.getVector3FromSpherical(theta, phi, r + 0.2f);
                Vector3 strandDirection = world.transform.position - strandPosition1;
                GameObject grassStrand;
                grassStrand = GameObject.Instantiate(grassBrand, strandPosition1, Quaternion.LookRotation(strandDirection)) as GameObject;
                grassStrand.tag = "GrassBrand";
                grassStrand.layer = 11;
                NetworkServer.Spawn(grassStrand);
            }

            if (!grassBitMap[theta2][phi2]) {
                grassBitMap[theta2][phi2] = true;
                Vector3 strandPosition2 = SphericalCoordinateHelper.getVector3FromSpherical(theta2, phi2, r);
                Vector3 strandDirection2 = world.transform.position - strandPosition2;
                GameObject grassStrand2;
                grassStrand2 = GameObject.Instantiate(grassBrand, strandPosition2, Quaternion.LookRotation(strandDirection2)) as GameObject;
                grassStrand2.tag = "GrassBrand";
                grassStrand2.layer = 11;
                NetworkServer.Spawn(grassStrand2);
            }


            if (theta % 2 == 0 && phi % 2 == 0 || theta % 2 > 0 && phi % 2 > 0) {
                theta += 1;
                theta2 -= 1;
            } else {
                phi += 1;
                phi2 -= 1;
            }

            if (theta == 360)
                theta = 0;
            if (theta2 == -1)
                theta2 = 359;
            if (phi == 180)
                phi = 0;
            if (phi == -1)
                phi2 = 179;

        }
    }
}

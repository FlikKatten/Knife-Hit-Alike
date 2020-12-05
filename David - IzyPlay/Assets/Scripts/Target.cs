using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //CRIA UM ELEMENTO DE PADRÃO QUE PERMITE EDITAR A CLASSE POR MEIO DO EDITOR
    [System.Serializable] public class targetRotation
    {
        public float speed, duration;
    }

    //PERMITE EDITAR VARIÁVEIS PRIVADAS POR MEIO DO EDITOR
    [SerializeField] public targetRotation[] rotationPattern;

    private WheelJoint2D wheelJoint;
    private JointMotor2D jointMotor;

    private void Start()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        jointMotor = new JointMotor2D();
        StartCoroutine("StartRotationPattern");
    }

    private IEnumerator StartRotationPattern()
    {
        int rotationIndex = 1;

        while (true)
        {
            yield return new WaitForFixedUpdate();

            jointMotor.motorSpeed = rotationPattern[rotationIndex].speed;

            //REAÇÃO DE FORÇA APLICADA
            jointMotor.maxMotorTorque = 10000;
            wheelJoint.motor = jointMotor;

            yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].duration);
            rotationIndex++;
            rotationIndex = rotationIndex < rotationPattern.Length ? rotationIndex : 1;

            if (UI.isGameOver)
            {
                rotationIndex = 0;
            }
            else if (UI.isGameWon)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                UI.isGameWon = false;
                GameManager.isNextStage = true;

                Destroy(gameObject);
            }
        }
    }
}
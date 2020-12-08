using UnityEditor;
using UnityEditor.Animations;

public class BlendTreeEditorExtensions
{
    private static BlendTree copiedBlendTree;

    [MenuItem("Assets/Copy Blend Tree")]
    private static void CopyBlendTree()
    {
        copiedBlendTree = Selection.activeObject as BlendTree;
    }

    [MenuItem("Assets/Paste Blend Tree")]
    private static void PasteBlendTree()
    {
        BlendTree selectedBlendTree = Selection.activeObject as BlendTree;

        UnityEditor.Undo.RecordObject(selectedBlendTree, "Paste Blend Tree");
        PasteBlendTreeHelper(selectedBlendTree, copiedBlendTree);
    }

    [MenuItem("Assets/Copy Blend Tree", true)]
    private static bool CopyBlendTreeValidate()
    {
        return Selection.activeObject is BlendTree;
    }

    [MenuItem("Assets/Paste Blend Tree", true)]
    private static bool PasteBlendTreeValidate()
    {
        return copiedBlendTree != null;
    }

    private static void PasteBlendTreeHelper(BlendTree parentBlendTree, BlendTree sourceBlendTree)
    {
        // Create a child tree in the destination parent, this seems to be the only way to correctly 
        // add a child tree as opposed to AddChild(motion)
        BlendTree pastedTree = parentBlendTree.CreateBlendTreeChild(parentBlendTree.maxThreshold);
        pastedTree.name = sourceBlendTree.name;
        pastedTree.blendType = sourceBlendTree.blendType;
        pastedTree.blendParameter = sourceBlendTree.blendParameter;
        pastedTree.blendParameterY = sourceBlendTree.blendParameterY;
        pastedTree.minThreshold = sourceBlendTree.minThreshold;
        pastedTree.maxThreshold = sourceBlendTree.maxThreshold;
        pastedTree.useAutomaticThresholds = sourceBlendTree.useAutomaticThresholds;

        // Recursively duplicate the tree structure
        // Motions can be directly added as references while trees must be recursively to avoid accidental sharing
        foreach (ChildMotion child in sourceBlendTree.children)
        {
            if (child.motion is BlendTree)
            {
                PasteBlendTreeHelper(pastedTree, child.motion as BlendTree);
            }
            else
            {
                pastedTree.AddChild(child.motion);
            }
        }
    }
}
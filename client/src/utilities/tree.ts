/* eslint-disable @typescript-eslint/no-explicit-any */
export function buildTree(data: any[] | [], parentId = 0) {
    const tree: any[] = [];
    data.forEach((item) => {
        if (item.parentId === parentId) {
            const children = buildTree(data, item.id);
            if (children.length) {
                item.subRows = children;
            }
            tree.push(item);
        }
    });
    return tree;
}



#include <iostream>
#include <vector>
using namespace std;

vector<int> twoSum(vector<int> &arr, int target){
  int size = arr.size();
  vector<int> v;
  for(int i = 0; i < size - 1; i++) {
    for (int j = i + 1; j < size; j++) {
    if(arr[i] + arr[j] == target){
        v.push_back(i);
        v.push_back(j);
        return  v;

      }
    }

}
  return v;
}
struct ListNode{
  int val;
  ListNode *next;
  ListNode() : val(0), next(nullptr) {}
  ListNode(int x) : val(x), next(nullptr) {}
  ListNode(int x, ListNode *next) : val(x),next(next){}
};
ListNode* addTwoNumbers(ListNode *l1, ListNode *l2 ){
  int car = 0;
  ListNode *result = new ListNode();
  ListNode *curr = result;
  while (l1||l2||car) {
    int d1 = l1 ? l1 -> val : 0;
    int d2 = l2 ? l2 -> val : 0;
    int sum = d1 + d2 +car;
    curr -> next = new ListNode(sum%10);
    curr = curr -> next;
    car = sum/10;
    l1 = l1 ? l1 -> next : nullptr;
    l2 = l2 ? l2 -> next :nullptr;
  }
return result -> next;
}
int main (int argc, char *argv[]) {
 vector <int> g= {1,24,55,7,46};
 vector<int> res =twoSum(g, 8);
  for (int i: res) {
  cout << " " << i;
  }
  return 0;
}
